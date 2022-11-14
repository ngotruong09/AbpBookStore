using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MyAbp.BookStore.Books
{
    public interface IBooksAppService : IApplicationService
    {
        Task<PagedResultDto<BookDto>> GetListAsync(GetBooksInput input);
        Task<BookDto> CreateAsync(BookCreateDto input);
        Task<BookDto> GetAsync(Guid id);
        Task<BookDto> UpdateAsync(Guid id, BookUpdateDto input);
        Task DeleteAsync(Guid id);
    }
}