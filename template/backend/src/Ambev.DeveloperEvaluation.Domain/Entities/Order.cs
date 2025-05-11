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
        if (orderItem.Quantity > 20)
            throw new DomainException("can't add more than 20 units of the same item.");
        
        OrderItems.Add(orderItem);
        ApplyTotalValue(orderItem);
    }
    
    private void ApplyTotalValue(OrderItem orderItem)
    {
        decimal discountForItem = 0.0m;
        
        if (orderItem.Quantity > 4 && orderItem.Quantity <= 9)
        {
            discountForItem = orderItem.UnitPrice * 0.10m;
        }
            
        if (orderItem.Quantity >= 10 && orderItem.Quantity <= 20)
        {
            discountForItem = orderItem.UnitPrice * 0.20m;
        }
            
        orderItem.UnitPrice -= discountForItem;
        
        var totalPriceWithDiscount = orderItem.UnitPrice * orderItem.Quantity;
        Total += totalPriceWithDiscount;
    }
}