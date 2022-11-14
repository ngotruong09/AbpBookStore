using Exporter;
using Exporter.Abstract.Managers;
using Exporter.Csv.Exporters;
using Exporter.Excel.Exporters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using MyAbp.BookStore.Permissions;
using MyAbp.BookStore.Services.Books;
using Newtonsoft.Json;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Volo.Abp.Content;

namespace MyAbp.BookStore.Books
{
    [Authorize(BookStorePermissions.Books.Default)]
    public class BooksAppService : ApplicationService, IBooksAppService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IDistributedCache<BookExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IExporterManager _exporterManager;

        public BooksAppService(IBookRepository bookRepository, IDistributedCache<BookExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IExporterManager exporterManager)
        {
            _bookRepository = bookRepository;
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _exporterManager = exporterManager;
        }

        public virtual async Task<PagedResultDto<BookDto>> GetListAsync(GetBooksInput input)
        {
            var totalCount = await _bookRepository.GetCountAsync(input.FilterText, input.Name, input.AuthorName, input.PriceMin, input.PriceMax, input.PublishDateMin, input.PublishDateMax);
            var items = await _bookRepository.GetListAsync(input.FilterText, input.Name, input.AuthorName, input.PriceMin, input.PriceMax, input.PublishDateMin, input.PublishDateMax, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<BookDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Book>, List<BookDto>>(items)
            };
        }

        [Authorize(BookStorePermissions.Books.Create)]
        public virtual async Task<BookDto> CreateAsync(BookCreateDto input)
        {
            var book = ObjectMapper.Map<BookCreateDto, Book>(input);
            book = await _bookRepository.InsertAsync(book, true);
            return ObjectMapper.Map<Book, BookDto>(book);
        }

        public virtual async Task<BookDto> GetAsync(Guid id)
        {
            var book = await _bookRepository.GetAsync(id);
            return ObjectMapper.Map<Book, BookDto>(book);
        }

        [Authorize(BookStorePermissions.Books.Edit)]
        public virtual async Task<BookDto> UpdateAsync(Guid id, BookUpdateDto input)
        {
            var queryable = await _bookRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);
            var book = await AsyncExecuter.FirstOrDefaultAsync(query);
            ObjectMapper.Map(input, book);
            book = await _bookRepository.UpdateAsync(book);
            return ObjectMapper.Map<Book, BookDto>(book);
        }

        [Authorize(BookStorePermissions.Books.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _bookRepository.DeleteAsync(id);
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new BookExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetFileAsync(BookDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _bookRepository.GetListAsync(input.FilterText, input.Name, input.Title, input.PriceMin, input.PriceMax);
            var datas = (from item in items
                         let json = JsonConvert.SerializeObject(item)
                         let tmp= JsonConvert.DeserializeObject<IDictionary<string, object>>(json)
                         select tmp).ToList();

            var memoryStream = new MemoryStream();
            var fileName = string.Empty;
            if (input.FileType == "csv")
            {
                memoryStream = await _exporterManager.GetStreamDocument(
                          ExportCsvType.GetExportType(), datas, new OptionCsv { TempFolderPath = Path.GetTempPath() });
                fileName = "Book.csv";
                memoryStream.Seek(0, SeekOrigin.Begin);
            }
            else if(input.FileType == "excel")
            {
                memoryStream = await _exporterManager.GetStreamDocument(
                          ExportExcelType.GetExportType(), datas, new OptionExcel { NumberRowPerSheet = 500000 });
                fileName = "Book.xlsx";
                memoryStream.Seek(0, SeekOrigin.Begin);
            }
            
            return new RemoteStreamContent(memoryStream, fileName);
        }
    }
}