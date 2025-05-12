using System.ComponentModel.DataAnnotations.Schema;
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class OrderItem : BaseEntity
{
    public OrderItem(Guid orderId, int quantity, Guid productId, decimal unitPrice)
    {
        OrderId = orderId;
        Quantity = quantity;
        ProductId = productId;
        UnitPrice = unitPrice;
    }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    
    public bool Cancelled { get; private set; } =  false;
    
    public Guid OrderId { get; private set; }
    public Guid ProductId { get; private set; }
    public Order Order { get; private set; } = null!;
    public Product Product { get; private set; } = null!;
    
    [NotMapped]
    public decimal FinalPrice { get; private set; }
    [NotMapped]
    public decimal ItemWithDiscount { get; set; }

    public void ApplyTotalValue()
    {
        decimal discount = 0.0m;
        
        if (Quantity > 4 && Quantity <= 9)
        {
            discount = UnitPrice * 0.10m;
        }
            
        if (Quantity >= 10 && Quantity <= 20)
        {
            discount = UnitPrice * 0.20m;
        }
            
        ItemWithDiscount = UnitPrice -  discount;
        
        var totalPriceWithDiscount = ItemWithDiscount * Quantity;
        FinalPrice += totalPriceWithDiscount;
    }

    public void UpdateItem(Guid productId, int quantity)
    {
       ProductId = productId;
       Quantity = quantity;
       ApplyTotalValue();
    }
}