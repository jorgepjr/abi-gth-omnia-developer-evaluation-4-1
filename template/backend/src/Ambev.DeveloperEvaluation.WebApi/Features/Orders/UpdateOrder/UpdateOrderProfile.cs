using Ambev.DeveloperEvaluation.Application.Orders.UpdateOrder;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.UpdateOrder;

public class UpdateOrderProfile : Profile
{
    public UpdateOrderProfile()
    {
        CreateMap<UpdateOrderRequest, UpdateOrderCommand>();
        CreateMap<UpdateOrderResult, UpdateOrderResponse>();
        
        CreateMap<UpdateOrderItemRequest, UpdateOrderItemCommand>();
    }
}