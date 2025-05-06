using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class OrderItem : BaseEntity
{
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public bool Cancelled { get; set; } =  false;
    
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public Order Order { get; set; } = null!;
    public Product Product { get; set; } = null!;
    
}