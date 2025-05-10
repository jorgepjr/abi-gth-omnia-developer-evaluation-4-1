using Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Validator;

public class OrderValidator : AbstractValidator<CreateOrderCommand>
{
    public OrderValidator()
    {
        RuleFor(order => order.OrderItems)
            .NotNull().NotEmpty().WithMessage("orderItems can't be empty");

        RuleFor(order => order.OrderItems).Must(ValidateDuplicateProduct)
            .WithMessage("The order contains duplicate items");
    }

    private static bool ValidateDuplicateProduct(List<CreateOrderItemCommand> createOrderItemCommands)
    {
        var groupByProductIds = createOrderItemCommands
            .GroupBy(x=>x.ProductId);

        return !groupByProductIds.Any(x => x.Count() > 1);
    }
}