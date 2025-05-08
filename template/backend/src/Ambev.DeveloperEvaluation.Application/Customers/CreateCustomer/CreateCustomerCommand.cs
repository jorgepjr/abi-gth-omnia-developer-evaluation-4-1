using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;

public class CreateCustomerCommand : IRequest<CreateCustomerResult>
{
    public string Name { get; set; } = string.Empty;
}