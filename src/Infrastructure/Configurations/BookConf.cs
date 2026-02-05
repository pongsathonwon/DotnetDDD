using Domain.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class BookConf : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Books");
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Title).IsRequired().HasMaxLength(200);
        builder.Property(b => b.Description).IsRequired();
        builder.OwnsOne(b => b.Isbn, isbn =>
        {
            isbn.Property(i => i.Value).HasColumnName("Isbn").IsRequired().HasMaxLength(13);
        });
        builder.OwnsOne(b => b.Price, price =>
        {
            price.Property(p => p.Amount).HasColumnName("PriceAmount").IsRequired().HasColumnType("decimal(18,2)");
            price.Property(p => p.Currency).HasColumnName("PriceCurrency").IsRequired().HasMaxLength(3);
        });
        builder.HasOne<Author>().WithMany().HasForeignKey(b => b.AuthorId);
        builder.HasOne<Category>().WithMany().HasForeignKey(b => b.CategoryId);
    }
}