using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Orders.UpdateOrder;

public class UpdateOrderProfile : Profile
{
    public UpdateOrderProfile()
    {
        CreateMap<UpdateOrderItemCommand, OrderItem>();
        
        CreateMap<UpdateOrderCommand, Order>();
        CreateMap<Order, UpdateOrderResult>();
    }
}