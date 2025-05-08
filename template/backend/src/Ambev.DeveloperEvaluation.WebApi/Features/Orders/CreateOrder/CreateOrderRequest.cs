namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder;

public class CreateOrderRequest
{
    public Guid CustomerId { get; set; }
    public Guid ShopId { get; set; }
    public List<CreateOrderItemRequest> OrderItems { get; set; } = [];
}