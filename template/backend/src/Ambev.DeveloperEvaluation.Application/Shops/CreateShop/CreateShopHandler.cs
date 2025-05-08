using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Shops.CreateShop;

public class CreateShopHandler : IRequestHandler<CreateShopCommand, CreateShopResult>
{
    private readonly IShopRepository _shopRepository;
    private readonly IMapper _mapper;

    public CreateShopHandler(IShopRepository shopRepository, IMapper mapper)
    {
        _shopRepository = shopRepository;
        _mapper = mapper;
    }

    public async Task<CreateShopResult> Handle(CreateShopCommand command, CancellationToken cancellationToken)
    {
        var shop = _mapper.Map<Shop>(command);
        var createShop = await _shopRepository.CreateAsync(shop, cancellationToken);
        var result = _mapper.Map<CreateShopResult>(createShop);
        return result;
    }
}