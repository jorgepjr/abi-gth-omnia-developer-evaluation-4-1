using Ambev.DeveloperEvaluation.Application.Validator;
using MediatR;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;

public class CreateOrderCommand : IRequest<CreateOrderResult>
{
    public Guid CustomerId { get; set; }
    public Guid ShopId { get; set; }
    public List<CreateOrderItemCommand> OrderItems { get; set; } = [];
    
    public ValidationResult ValidateOrder()
    {
        var validator = new OrderValidator();
        var validationResult = validator.Validate(this);
        return validationResult;
    }
}