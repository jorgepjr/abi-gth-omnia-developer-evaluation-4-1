using Ambev.DeveloperEvaluation.Application.Orders.CancelOrder;
using Ambev.DeveloperEvaluation.Application.Orders.CancelOrderItems;
using Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;
using Ambev.DeveloperEvaluation.Application.Orders.GetById;
using Ambev.DeveloperEvaluation.Application.Orders.UpdateOrder;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder;
using Ambev.DeveloperEvaluation.WebApi.Features.Orders.UpdateOrder;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders;

[ApiController]
[ApiExplorerSettings(GroupName = "app"), Tags("4 - Orders")]
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
    
    [HttpPut("/{orderId}")]
    public async Task<IActionResult> UpdateOrder(Guid orderId, [FromBody] UpdateOrderRequest request, CancellationToken cancellationToken)
    {
        request.OrderId = orderId;
        var command = _mapper.Map<UpdateOrderCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

       return Ok(response); 
    }
    
    [HttpPut("/{orderId}/cancel")]
    public async Task<IActionResult> Cancel(Guid orderId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new CancelOrderCommand{OrderId = orderId}, cancellationToken);
        return Ok(response); 
    }
    
    [HttpPut("/{orderId}/cancel-items")]
    public async Task<IActionResult> CancelOrderItems(Guid orderId, List<Guid?>? orderItemsIds, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(
            new CancelOrderItemsCommand{OrderId = orderId, OrderItemsIds = orderItemsIds}, cancellationToken);
        return Ok(response); 
    }
    
    [HttpGet]
    public async Task<IActionResult> GetById(Guid orderId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetOrderbyIdCommand{OrderId = orderId}, cancellationToken); 
        return Ok(response);
    }
}