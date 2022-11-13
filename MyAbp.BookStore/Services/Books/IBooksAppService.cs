using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MyAbp.BookStore.Books
{
    public interface IBooksAppService : IApplicationService
    {
        Task<PagedResultDto<BookDto>> GetListAsync(GetBooksInput input);
    }
}