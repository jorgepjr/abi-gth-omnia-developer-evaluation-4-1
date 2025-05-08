using Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Shops.CreateShop;

public class CreateShopHandler : IRequestHandler<CreateShopCommand, CreateShopResult>
{
    private readonly IShopRepository _customerRepository;
    private readonly IMapper _mapper;

    public CreateShopHandler(IShopRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<CreateShopResult> Handle(CreateShopCommand command, CancellationToken cancellationToken)
    {
        var customer = _mapper.Map<Shop>(command);
        var createShop = await _customerRepository.CreateAsync(customer, cancellationToken);
        var result = _mapper.Map<CreateShopResult>(createShop);
        return result;
    }
}