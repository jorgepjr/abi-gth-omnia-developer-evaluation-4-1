using Ambev.DeveloperEvaluation.Application.Validator;
using MediatR;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Ambev.DeveloperEvaluation.Application.Orders.UpdateOrder;

public class UpdateOrderCommand : IRequest<UpdateOrderResult>
{
    public Guid OrderId { get; set; }
    public List<UpdateOrderItemCommand> OrderItems { get; set; } = [];


    // public ValidationResult ValidateOrder()
    // {
    //     var validator = new OrderValidator();
    //     var validationResult = validator.Validate(this);
    //     return validationResult;
    // }
}