using Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;

namespace Ambev.DeveloperEvaluation.Application.Orders.GetById;

public class GetOrderByReult
{
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid ShopId { get; set; }
    public IEnumerable<CreateOrderItemResult> OrderItems { get; set; } = [];
    public long OrderNumber { get; set; }
    public string Store { get; set; } = string.Empty;
    public string Customer { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public decimal Total { get; set; }
}