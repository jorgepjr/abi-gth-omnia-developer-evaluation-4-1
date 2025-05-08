namespace Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;

public class CreateOrderItemCommand
{
    public decimal UnitPrice { get; set; }
    public Guid ProductId { get; set; }
}