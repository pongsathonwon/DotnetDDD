using Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CustomerConf : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(c => c.LastName).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Email).IsRequired().HasMaxLength(200);
        builder.OwnsOne(c => c.Address, address =>
        {
            address.Property(a => a.Street).HasColumnName("Street").IsRequired().HasMaxLength(200);
            address.Property(a => a.City).HasColumnName("City").IsRequired().HasMaxLength(100);
            address.Property(a => a.State).HasColumnName("State").IsRequired().HasMaxLength(100);
            address.Property(a => a.ZipCode).HasColumnName("ZipCode").IsRequired().HasMaxLength(5);
            address.Property(a => a.Country).HasColumnName("Country").IsRequired().HasMaxLength(100);
        });
    }
}