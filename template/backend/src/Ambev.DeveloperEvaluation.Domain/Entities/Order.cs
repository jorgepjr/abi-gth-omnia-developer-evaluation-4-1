using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Order : BaseEntity
{
    public Order()
    {
        Id = Guid.NewGuid();
    }

    public long Number { get; set; }
    public DateTime CreateAt { get;  set; } = DateTime.Now;
    public decimal Total{ get; set; }
    public bool Cancelled { get; set; } = false;
    public List<OrderItem> OrderItems { get; set; } = [];
    
    public Guid CustomerId { get; set; }
    public Guid ShopId { get; set; }
    public Customer Customer { get; set; } = null!;
    public Shop Shop { get; set; } = null!;

    public void AddOrderItem(OrderItem orderItem)
    {
        OrderItems.Add(orderItem);
        SetDiscount();
    }
    
    private void SetDiscount()
    {
        decimal discountForItem = 0.0m;
        
        foreach (var item in OrderItems)
        {
            if (item.Quantity > 4)
            {
                discountForItem = item.UnitPrice * 0.10m;
            }
            
            if (item.Quantity >= 10 && item.Quantity <= 20)
            {
                discountForItem = item.UnitPrice * 0.20m;
            }
            
            item.UnitPrice -= discountForItem;
        }
        
        Total = OrderItems.Sum(x => x.UnitPrice * x.Quantity);
    }
}