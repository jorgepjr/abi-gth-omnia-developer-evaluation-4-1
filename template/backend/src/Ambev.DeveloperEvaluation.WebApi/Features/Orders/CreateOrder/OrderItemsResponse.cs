namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder;

public class OrderItemsResponse
{
    public int Quantity { get; set; }
    public Guid ProductId { get; set; }
    public decimal UnitPrice { get; set; }
}