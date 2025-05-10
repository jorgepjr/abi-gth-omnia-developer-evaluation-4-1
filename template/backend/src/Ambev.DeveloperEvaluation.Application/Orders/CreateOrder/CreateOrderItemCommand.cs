using Ambev.DeveloperEvaluation.Application.Validator;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;

public class CreateOrderItemCommand
{
    public decimal UnitPrice { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    
    public ValidationResult ValidateOrderItems()
    {
        var validator = new OrderItemValidator();
        var validationResult = validator.Validate(this);
        return validationResult;
    }
}