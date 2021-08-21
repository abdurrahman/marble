using Marble.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marble.Infrastructure.Data.Maps
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Id)
                .HasColumnName("Id");
            
            builder.Property(x => x.Title)
                .HasColumnType("varchar(200)")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        }
    }
}