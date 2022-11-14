using Microsoft.AspNetCore.Mvc;
using MyAbp.BookStore.Books;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace MyAbp6.Web.Pages.Books
{
    public class CreateModalModel : AbpPageModel
    {
        [BindProperty]
        public BookCreateViewModel Book { get; set; }

        private readonly IBooksAppService _booksAppService;

        public CreateModalModel(IBooksAppService booksAppService)
        {
            _booksAppService = booksAppService;
        }

        public async Task OnGetAsync()
        {
            Book = new BookCreateViewModel();
            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var book = ObjectMapper.Map<BookCreateViewModel, BookCreateDto>(Book);
            await _booksAppService.CreateAsync(book);
            return NoContent();
        }
    }

    public class BookCreateViewModel : BookCreateDto
    {
    }
}