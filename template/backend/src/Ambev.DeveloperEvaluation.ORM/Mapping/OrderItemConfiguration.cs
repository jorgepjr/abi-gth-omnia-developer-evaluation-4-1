using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Quantity)
            .IsRequired();
        
        builder.Property(x => x.UnitPrice).IsRequired();
        builder.Property(x => x.Cancelled).IsRequired();
    }
}