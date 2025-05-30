using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Total)
            .IsRequired()
            .HasColumnType("MONEY")
            .HasColumnType("decimal(18, 2)");

        builder.Property(x => x.CreateAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.Property(x => x.CanceledAt);
        
        builder.Property(x => x.Cancelled).IsRequired();
    }
}