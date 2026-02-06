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
        builder.Property(b => b.Isbn)
            .HasConversion(
                isbn => isbn.Value,
                value => new Isbn(value))
            .IsRequired()
            .HasMaxLength(13);


        builder.OwnsOne(b => b.Price, priceBuilder =>
        {
            priceBuilder.Property(p => p.Amount)
                .HasColumnName("PriceAmount")
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            priceBuilder.Property(p => p.Currency)
                .HasColumnName("PriceCurrency")
                .IsRequired()
                .HasMaxLength(3);
        });
        builder.HasOne<Author>().WithMany().HasForeignKey(b => b.AuthorId);
        builder.HasOne<Category>().WithMany().HasForeignKey(b => b.CategoryId);
    }
}