using JetBrains.Annotations;
using Volo.Abp.Domain.Entities;

namespace MyAbp.BookStore.Books
{
    public class Book: Entity<Guid>
    {
        [CanBeNull]
        public virtual string Name { get; set; }

        [CanBeNull]
        public virtual string AuthorName { get; set; }

        public virtual decimal Price { get; set; }

        public virtual DateTime PublishDate { get; set; }
    }
}
