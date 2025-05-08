using Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Shops.CreateShop;

public class CreateShopCommand : IRequest<CreateShopResult>
{
    public string TradeName { get; set; } = string.Empty;
}