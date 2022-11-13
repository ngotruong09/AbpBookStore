using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using MyAbp.BookStore.Data;

namespace MyAbp.BookStore.Books
{
    public class EfCoreBookRepository : EfCoreRepository<BookStoreDbContext, Book, Guid>, IBookRepository
    {
        public EfCoreBookRepository(IDbContextProvider<BookStoreDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<Book>> GetListAsync(
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
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, name, authorName, priceMin, priceMax, publishDateMin, publishDateMax);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string name = null,
            string authorName = null,
            decimal? priceMin = null,
            decimal? priceMax = null,
            DateTime? publishDateMin = null,
            DateTime? publishDateMax = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, name, authorName, priceMin, priceMax, publishDateMin, publishDateMax);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Book> ApplyFilter(
            IQueryable<Book> query,
            string filterText,
            string name = null,
            string authorName = null,
            decimal? priceMin = null,
            decimal? priceMax = null,
            DateTime? publishDateMin = null,
            DateTime? publishDateMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name.Contains(filterText) || e.AuthorName.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(authorName), e => e.AuthorName.Contains(authorName))
                    .WhereIf(priceMin.HasValue, e => e.Price >= priceMin.Value)
                    .WhereIf(priceMax.HasValue, e => e.Price <= priceMax.Value)
                    .WhereIf(publishDateMin.HasValue, e => e.PublishDate >= publishDateMin.Value)
                    .WhereIf(publishDateMax.HasValue, e => e.PublishDate <= publishDateMax.Value);
        }
    }
}