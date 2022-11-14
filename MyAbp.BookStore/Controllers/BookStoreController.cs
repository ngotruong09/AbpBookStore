using MyAbp.BookStore.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace MyAbp.BookStore.Controllers
{
    public class BookStoreController: AbpController
    {
        public BookStoreController()
        {
            LocalizationResource = typeof(BookStoreResource);
        }
    }
}
