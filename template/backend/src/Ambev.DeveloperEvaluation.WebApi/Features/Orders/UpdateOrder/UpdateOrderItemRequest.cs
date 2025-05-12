namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.UpdateOrder;

public class UpdateOrderItemRequest
{
    public Guid OrderItemId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}