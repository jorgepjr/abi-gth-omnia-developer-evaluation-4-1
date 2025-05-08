using Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders;

[ApiController]
[ApiExplorerSettings(GroupName = "app")]
[Route("api/[controller]")]
public class OrdersController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public OrdersController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateOrderCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateOrderResponse>
        {
            Success = true,
            Message = "Order created successfully",
            Data = new CreateOrderResponse
            {
                OrderId = response.OrderId,
                ShopId = response.ShopId,
                CustomerId = response.CustomerId,
                OrderItems = response.OrderItems.Select(x=> new OrderItemsResponse
                {
                    UnitPrice = x.UnitPrice,
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                }),
                TotalPrice = response.Total,
            },
        }); 
    }
}