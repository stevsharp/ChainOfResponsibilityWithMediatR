using ChainOfResponsibility.Domain.PurchaseOrders;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChainOfResponsibility.Infrastructure.Persistence.Configurations;

public sealed class PurchaseOrderConfiguration : IEntityTypeConfiguration<PurchaseOrder>
{
    public void Configure(EntityTypeBuilder<PurchaseOrder> b)
    {
        b.ToTable("PurchaseOrders");
        b.HasKey(x => x.Id);
        b.Property(x => x.Name).HasMaxLength(200).IsRequired();
        b.Property(x => x.Amount).IsRequired();
        b.Property(x => x.Price).HasColumnType("TEXT").IsRequired();
        b.Property(x => x.Status)
            .HasConversion<string>()
            .HasMaxLength(30)
            .IsRequired();
        b.Property(x => x.ApprovedBy).HasMaxLength(50);
        b.Property(x => x.CreatedAtUtc).IsRequired();
        b.HasIndex(x => x.Status);
        b.HasIndex(x => x.CreatedAtUtc);
    }
}

