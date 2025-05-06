namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Order
{
    public long Id { get; set; }
    public DateTime CreateAt { get;  set; } = DateTime.Now;
    public decimal Total{ get; set; }
    public bool Cancelled { get; set; } = false;
    public List<OrderItem> OrderItems { get; set; } = [];
    
    public int CustomerId { get; set; }
    public int Shop { get; set; }
}