using Domain.Auth;
using Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class AuthRecordConf : IEntityTypeConfiguration<AuthRecord>
{
    public void Configure(EntityTypeBuilder<AuthRecord> builder)
    {
        builder.ToTable("AuthRecords");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Username).IsRequired().HasMaxLength(100);

        builder.Property(a => a.PasswordHash).IsRequired();
        builder.Property(a => a.Role)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);
        builder.Property(a => a.CustomerId).IsRequired();
        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(a => a.CustomerId);

        builder.HasIndex(a => a.Username).IsUnique();
    }
}