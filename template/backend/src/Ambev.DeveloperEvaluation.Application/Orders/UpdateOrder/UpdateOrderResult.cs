namespace Ambev.DeveloperEvaluation.Application.Orders.UpdateOrder;

public class UpdateOrderResult
{
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid ShopId { get; set; }
    public IEnumerable<UpdateOrderItemResult> OrderItems { get; set; } = [];
    public decimal Total { get; set; }
}