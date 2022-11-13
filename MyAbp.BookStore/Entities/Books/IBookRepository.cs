using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MyAbp.BookStore.Books
{
    public interface IBookRepository : IRepository<Book, Guid>
    {
        Task<List<Book>> GetListAsync(
            string filterText = null,
            string name = null,
            string authorName = null,
            decimal? priceMin = null,
            decimal? priceMax = null,
            DateTime? publishDateMin = null,
            DateTime? publishDateMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string name = null,
            string authorName = null,
            decimal? priceMin = null,
            decimal? priceMax = null,
            DateTime? publishDateMin = null,
            DateTime? publishDateMax = null,
            CancellationToken cancellationToken = default);
    }
}