using BugStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugStore.Data.Configurations;

public sealed class OrderLineConfigurations
    : IEntityTypeConfiguration<OrderLine>
{
    public void Configure(EntityTypeBuilder<OrderLine> builder)
    {
        builder.ToTable(nameof(OrderLine));

        builder.HasKey(l => l.Id);

        builder.Property(l => l.Quantity)
            .IsRequired(true);

        builder.Property(l => l.Total)
            .IsRequired(true);

        builder.HasOne(l => l.Product)
            .WithOne()
            .HasForeignKey<OrderLine>(l => l.ProductId)
            .IsRequired();
    }
}
