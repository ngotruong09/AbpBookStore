namespace MyAbp.BookStore.Books
{
    public class BookCreateDto
    {
        public virtual string Name { get; set; }
        public virtual string AuthorName { get; set; }
        public virtual decimal Price { get; set; }
        public virtual DateTime PublishDate { get; set; }
    }
}
