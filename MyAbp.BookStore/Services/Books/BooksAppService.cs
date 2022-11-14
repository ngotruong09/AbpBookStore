using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using MyAbp.BookStore.Permissions;
using MyAbp.BookStore.Services.Books;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;

namespace MyAbp.BookStore.Books
{
    [Authorize(BookStorePermissions.Books.Default)]
    public class BooksAppService : ApplicationService, IBooksAppService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IDistributedCache<BookExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;

        public BooksAppService(IBookRepository bookRepository, IDistributedCache<BookExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _bookRepository = bookRepository;
            _excelDownloadTokenCache = excelDownloadTokenCache;
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
    }
}