using AutoMapper;
using MyAbp.BookStore.Books;

namespace MyAbp.BookStore.ObjectMapping;

public class BookStoreAutoMapperProfile : Profile
{
    public BookStoreAutoMapperProfile()
    {
        /* Create your AutoMapper object mappings here */
        CreateMap<Book, BookDto>();
    }
}
