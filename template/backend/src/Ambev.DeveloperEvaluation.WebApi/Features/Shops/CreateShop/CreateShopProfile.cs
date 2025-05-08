using Ambev.DeveloperEvaluation.Application.Shops.CreateShop;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Shops.CreateShop;

public class CreateShopProfile : Profile
{
    public CreateShopProfile()
    {
        CreateMap<CreateShopRequest, CreateShopCommand>();
        CreateMap<CreateShopResult, CreateShopResponse>();
    }
}