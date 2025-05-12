using Ambev.DeveloperEvaluation.Application.Orders.CancelOrder;
using Ambev.DeveloperEvaluation.Application.Orders.CancelOrderItems;
using Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;
using Ambev.DeveloperEvaluation.Application.Orders.DeleteOrder;
using Ambev.DeveloperEvaluation.Application.Orders.GetAllOrders;
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

    /// <summary>Create order and add order items</summary>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [EndpointSummary("Create order and add order items")]
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
    
    /// <summary>Update order</summary>
    [ProducesResponseType(typeof(UpdateOrderResult), StatusCodes.Status200OK)]
    [EndpointSummary("Update order")]
    [HttpPut("{orderId}")]
    public async Task<IActionResult> UpdateOrder(Guid orderId, [FromBody] UpdateOrderRequest request, CancellationToken cancellationToken)
    {
        request.OrderId = orderId;
        var command = _mapper.Map<UpdateOrderCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

       return Ok(response); 
    }
    
    /// <summary>Cancel order  by order id </summary>
    [ProducesResponseType(typeof(List<CancelOrderItemsReult>), StatusCodes.Status200OK)]
    [EndpointSummary("Cancel order by orderId")]
    [HttpPut("{orderId}/cancel")]
    public async Task<IActionResult> Cancel(Guid orderId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new CancelOrderCommand{OrderId = orderId}, cancellationToken);
        return Ok(response); 
    }
    
    /// <summary>Cancel order items by order id and orderItems</summary>
    [ProducesResponseType(typeof(List<CancelOrderItemsReult>), StatusCodes.Status200OK)]
    [EndpointSummary("Cancel order items by orderId and orderItems")]
    [HttpPut("{orderId}/cancel-items")]
    public async Task<IActionResult> CancelOrderItems(Guid orderId, List<Guid?>? orderItemsIds, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(
            new CancelOrderItemsCommand{OrderId = orderId, OrderItemsIds = orderItemsIds}, cancellationToken);
        return Ok(response); 
    }

    /// <summary>
    /// Get order by orderId
    /// </summary>
    /// <param name="orderId">orderId</param>
    /// <param name="cancellationToken"></param>
    [ProducesResponseType(typeof(GetOrderByReult), StatusCodes.Status200OK)]
    [EndpointSummary("Get order by id")]
    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetById(Guid orderId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetOrderbyIdCommand{OrderId = orderId}, cancellationToken); 
        return Ok(response);
    }
    
    /// <summary>Remove order by orderId</summary>
    /// <param name="orderId">orderId</param>
    [ProducesResponseType(typeof(DeleteOrderReult), StatusCodes.Status200OK)]
    [EndpointSummary("Delete order by id")]
    [HttpDelete("{orderId}")]
    public async Task<IActionResult> Delete(Guid orderId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new DeleteOrderCommand(){OrderId = orderId}, cancellationToken); 
        return Ok(response);
    }
    
    /// <summary>
    /// Returns a paginated list of orders.
    /// </summary>
    /// <param name="pageNumber">Page number</param>
    /// <param name="pageSize">Number of items per page</param>
    /// <param name="cancellationToken"></param>
    [HttpGet("{pageNumber}/{pageSize}")]
    [ProducesResponseType(typeof(GetAllOrdersReult), StatusCodes.Status200OK)]
    [EndpointSummary("Get all orders with pagination")]
    public async Task<IActionResult> GetAllOrders(int pageSize, int pageNumber, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(
            new GetAllOrdersCommand{PageSize = pageSize, PageNumber = pageNumber}, cancellationToken); 
        return Ok(response);
    }
}