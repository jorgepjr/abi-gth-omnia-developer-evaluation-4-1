namespace Ambev.DeveloperEvaluation.Application.Orders.CancelOrderItems;

public class CancelOrderItemsReult
{
    public bool Cancelled { get; set; }
    public Guid OrderItemId { get; set; }
}