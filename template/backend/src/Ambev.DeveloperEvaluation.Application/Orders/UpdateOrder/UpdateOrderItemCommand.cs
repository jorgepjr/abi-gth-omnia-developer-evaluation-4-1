using Ambev.DeveloperEvaluation.Application.Validator;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Ambev.DeveloperEvaluation.Application.Orders.UpdateOrder;

public class UpdateOrderItemCommand
{
    public Guid OrderItemId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }

    // public ValidationResult ValidateOrderItems()
    // {
    //     var validator = new OrderItemValidator();
    //     var validationResult = validator.Validate(this);
    //     return validationResult;
    // }
}