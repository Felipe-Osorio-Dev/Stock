using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockAPI.Entities;

namespace StockAPI.Data.Config
{
    public class ProductEntityConfig : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.ToTable("Produtos");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Ean)
                .IsRequired()
                .HasMaxLength(13);

            builder.Property(p => p.Amount)
                .IsRequired();

            builder.Property(p => p.Validate)
                .IsRequired();

            builder.HasIndex(p => p.Ean)
                .IsUnique();
        }
    }
}
