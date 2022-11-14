using Microsoft.AspNetCore.Mvc;
using MyAbp.BookStore.Controllers;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace MyAbp.BookStore.Books
{
    [RemoteService(Name = "BookStore")]
    [Area("BookStore")]
    [ControllerName("Book")]
    [Route("api/book-store/books")]
    public class BookController : BookStoreController, IBooksAppService
    {
        private readonly IBooksAppService _booksAppService;
      
        public BookController(IBooksAppService booksAppService)
        {
            _booksAppService = booksAppService;
        }

        [HttpPost]
        [Route("create")]
        public Task<BookDto> CreateAsync(BookCreateDto input)
        {
            return _booksAppService.CreateAsync(input);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public Task DeleteAsync(Guid id)
        {
            return _booksAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("getbook/{id}")]
        public Task<BookDto> GetAsync(Guid id)
        {
            return _booksAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("get-download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _booksAppService.GetDownloadTokenAsync();
        }

        [HttpGet]
        [Route("get-list")]
        public Task<PagedResultDto<BookDto>> GetListAsync(GetBooksInput input)
        {
            return _booksAppService.GetListAsync(input);
        }

        [HttpPut]
        [Route("update/{id}")]
        public Task<BookDto> UpdateAsync(Guid id, BookUpdateDto input)
        {
            return _booksAppService.UpdateAsync(id, input);
        }
    }
}
