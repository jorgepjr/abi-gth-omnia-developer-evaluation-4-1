using Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.GetAllOrders;

public class GetAllOrdersCommand : IRequest<GetAllOrdersReult>
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}