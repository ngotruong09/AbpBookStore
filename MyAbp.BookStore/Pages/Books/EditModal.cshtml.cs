using Microsoft.AspNetCore.Mvc;
using MyAbp.BookStore.Books;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace MyAbp6.Web.Pages.Books
{
    public class EditModalModel : AbpPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public BookUpdateViewModel Book { get; set; }

        private readonly IBooksAppService _booksAppService;

        public EditModalModel(IBooksAppService booksAppService)
        {
            _booksAppService = booksAppService;
        }

        public async Task OnGetAsync()
        {
            var book = await _booksAppService.GetAsync(Id);
            Book = ObjectMapper.Map<BookDto, BookUpdateViewModel>(book);
        }

        public async Task<NoContentResult> OnPostAsync()
        {
            var book = ObjectMapper.Map<BookUpdateViewModel, BookUpdateDto>(Book);
            await _booksAppService.UpdateAsync(Id, book);
            return NoContent();
        }
    }

    public class BookUpdateViewModel : BookUpdateDto
    {
    }
}