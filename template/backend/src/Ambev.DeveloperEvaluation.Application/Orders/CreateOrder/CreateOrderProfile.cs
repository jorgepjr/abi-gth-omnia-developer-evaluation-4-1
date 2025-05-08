using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;

public class CreateOrderProfile : Profile
{
    public CreateOrderProfile()
    {
        CreateMap<CreateOrderItemCommand, OrderItem>();
        
        CreateMap<CreateOrderCommand, Order>();
        CreateMap<Order, CreateOrderResult>();
    }
}