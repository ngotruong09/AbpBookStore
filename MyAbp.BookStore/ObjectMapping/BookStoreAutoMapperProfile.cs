using AutoMapper;
using MyAbp.BookStore.Books;
using MyAbp6.Web.Pages.Books;
using Volo.Abp.AutoMapper;

namespace MyAbp.BookStore.ObjectMapping;

public class BookStoreAutoMapperProfile : Profile
{
    public BookStoreAutoMapperProfile()
    {
        /* Create your AutoMapper object mappings here */
        CreateMap<Book, BookDto>();

        CreateMap<BookCreateDto, Book>()
            .Ignore(x => x.Id);
        CreateMap<BookCreateViewModel, BookCreateDto>();

        CreateMap<BookDto, BookUpdateViewModel>();
        CreateMap<BookUpdateDto, Book>()
            .Ignore(x => x.Id);
        CreateMap<BookUpdateViewModel, BookUpdateDto>();
    }
}
