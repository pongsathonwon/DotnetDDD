using Domain.Catalog;
using Domain.Ordering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class OrderLineConf : IEntityTypeConfiguration<OrderLine>
{
    public void Configure(EntityTypeBuilder<OrderLine> builder)
    {
        builder.ToTable("OrderLines");
        builder.HasKey(ol => ol.Id);
        builder.Property(ol => ol.Quantity).IsRequired();
        builder.OwnsOne(ol => ol.UnitPrice, unitPrice =>
        {
            unitPrice.Property(u => u.Amount).HasColumnName("UnitPriceAmount").IsRequired().HasColumnType("decimal(18,2)");
            unitPrice.Property(u => u.Currency).HasColumnName("UnitPriceCurrency").IsRequired().HasMaxLength(3);
        });
        builder.HasOne<Book>().WithMany().HasForeignKey(ol => ol.BookId);

        builder.Ignore(ol => ol.LineTotal);
    }
}