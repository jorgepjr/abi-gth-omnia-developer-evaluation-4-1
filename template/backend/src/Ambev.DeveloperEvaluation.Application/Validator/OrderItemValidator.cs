using Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Validator;

public class OrderItemValidator : AbstractValidator<CreateOrderItemCommand>
{
    public OrderItemValidator()
    {
        RuleFor(orderItem => orderItem.Quantity)
            .LessThanOrEqualTo(20).WithMessage("can't add more than 20 units of the same item");
        
        RuleFor(orderItem => orderItem.Quantity)
            .GreaterThan(0).WithMessage("Invalid quantity. Enter value greater than zero");
    }
}