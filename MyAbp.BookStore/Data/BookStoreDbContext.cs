using Microsoft.EntityFrameworkCore;
using MyAbp.BookStore.Books;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace MyAbp.BookStore.Data;

public class BookStoreDbContext : AbpDbContext<BookStoreDbContext>
{
    public DbSet<Book> Books { get; set; }
    public const string DbTablePrefix = "App";
    public const string DbSchema = null;
    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own entities here */
        if (builder.IsHostDatabase())
        {
            builder.Entity<Book>(b =>
            {
                b.ToTable(DbTablePrefix + "Books", DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).HasColumnName(nameof(Book.Name));
                b.Property(x => x.AuthorName).HasColumnName(nameof(Book.AuthorName));
                b.Property(x => x.Price).HasColumnName(nameof(Book.Price));
                b.Property(x => x.PublishDate).HasColumnName(nameof(Book.PublishDate));
            });
        }
    }
}
