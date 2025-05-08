namespace Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;

public class CreateOrderResult
{
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid ShopId { get; set; }
    public IEnumerable<CreateOrderItemResult> OrderItems { get; set; } = [];
    public decimal Total { get; set; }
}