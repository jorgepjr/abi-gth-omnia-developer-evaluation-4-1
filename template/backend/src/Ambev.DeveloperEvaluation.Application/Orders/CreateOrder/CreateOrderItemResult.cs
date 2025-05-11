namespace Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;

public class CreateOrderItemResult
{
    public int Quantity { get; set; }
    public Guid ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal FinalPrice { get; set; }
    public bool Cancelled { get; set; }
}