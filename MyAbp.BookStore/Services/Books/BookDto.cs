using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace MyAbp.BookStore.Books
{
    public class BookDto : EntityDto<Guid>
    {
        public virtual string Name { get; set; }
        public virtual string AuthorName { get; set; }
        public virtual decimal Price { get; set; }
        public virtual DateTime PublishDate { get; set; }
    }
}