using Volo.Abp.Application.Dtos;
using System;

namespace MyAbp.BookStore.Books
{
    public class GetBooksInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string Name { get; set; }
        public string AuthorName { get; set; }
        public decimal? PriceMin { get; set; }
        public decimal? PriceMax { get; set; }
        public DateTime? PublishDateMin { get; set; }
        public DateTime? PublishDateMax { get; set; }

        public GetBooksInput()
        {

        }
    }
}