namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder;

public class CreateOrderResponse
{
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid ShopId { get; set; }
    public IEnumerable<OrderItemsResponse> OrderItems { get; set; } = [];
    public decimal TotalPrice { get; set; }
}