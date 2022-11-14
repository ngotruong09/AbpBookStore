namespace MyAbp.BookStore.Services.Books
{
    public class BookDownloadDto
    {
        public string DownloadToken { get; set; }
        public string FileType { get; set; }
        public string FilterText { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public decimal? PriceMin { get; set; }
        public decimal? PriceMax { get; set; }
    }
}
