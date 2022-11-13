using Microsoft.AspNetCore.Authorization;
using MyAbp.BookStore.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MyAbp.BookStore.Books
{
    [Authorize(BookStorePermissions.Books.Default)]
    public class BooksAppService : ApplicationService, IBooksAppService
    {
        private readonly IBookRepository _bookRepository;

        public BooksAppService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
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
    }
}