using Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.UpdateOrder;

public class UpdateOrderResponse
{
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid ShopId { get; set; }
    public IEnumerable<OrderItemsResponse> OrderItems { get; set; } = [];
    public decimal TotalPrice { get; set; }
}