using Domain.Catalog;
using Domain.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class StockItemConf : IEntityTypeConfiguration<StockItem>
{
    public void Configure(EntityTypeBuilder<StockItem> builder)
    {
        builder.ToTable("StockItems");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.QuantityInStock).IsRequired();
        builder.Property(s => s.ReservedQuantity).IsRequired();
        builder.HasOne<Book>().WithMany().HasForeignKey(s => s.BookId);
    }
}
