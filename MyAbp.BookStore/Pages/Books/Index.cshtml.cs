using MyAbp.BookStore.Books;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace MyAbp.BookStore.Web.Pages.Books
{
    public class IndexModel : AbpPageModel
    {
        public string NameFilter { get; set; }
        public string AuthorNameFilter { get; set; }
        public decimal? PriceFilterMin { get; set; }

        public decimal? PriceFilterMax { get; set; }
        public DateTime? PublishDateFilterMin { get; set; }

        public DateTime? PublishDateFilterMax { get; set; }

        private readonly IBooksAppService _booksAppService;

        public IndexModel(IBooksAppService booksAppService)
        {
            _booksAppService = booksAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}