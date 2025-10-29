using BugStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugStore.Data.Configurations;

public class ProductConfigurarion
    : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(nameof(Product));

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Title)
            .IsRequired();

        builder.Property(p => p.Description)
            .IsRequired(false);

        builder.Property(p => p.Slug)
            .IsRequired();

        builder.Property(p => p.Price)
            .IsRequired();
    }
}
