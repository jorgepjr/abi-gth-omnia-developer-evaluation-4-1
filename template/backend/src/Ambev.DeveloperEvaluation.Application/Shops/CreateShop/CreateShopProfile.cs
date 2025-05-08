using Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Shops.CreateShop;

public class CreateShopProfile : Profile
{
    public CreateShopProfile()
    {
        CreateMap<CreateShopCommand, Shop>();
        CreateMap<Shop, CreateShopResult>();
    }
}