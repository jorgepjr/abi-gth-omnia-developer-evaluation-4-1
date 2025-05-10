using Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class CreateOrderHandlerTests
{
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    
    public CreateOrderHandlerTests()
    {
        _mapper = Substitute.For<IMapper>();
        _orderRepository = Substitute.For<IOrderRepository>();
        _productRepository = Substitute.For<IProductRepository>();
    }
    [Fact(DisplayName = "register orders and add order item")]
    public async Task MustCreateOrdersAndAddOrderItems()
    {
        //Arrange
        var expectedResult = 302.5m;
        
        _orderRepository.CreateAsync(Arg.Any<Order>())
            .Returns(new Order{CustomerId = Guid.NewGuid(), ShopId = Guid.NewGuid()});
        
        _productRepository.GetById(Arg.Any<Guid>())
            .Returns(new Product());

        var handler = new CreateOrderHandler(_orderRepository, _mapper, _productRepository);
        var createCommand = GetCreateOrderCommand();

        //Action
        var res = await handler.Handle(createCommand, default);
        
        //Assert
        Assert.Equal(expectedResult, res.Total);
    }

    private static CreateOrderCommand GetCreateOrderCommand()
    {
        var createCommand = new CreateOrderCommand
        {
            CustomerId = Guid.NewGuid(),
            ShopId = Guid.NewGuid(),
            OrderItems =
            [
                new CreateOrderItemCommand
                {
                    ProductId = new Guid("7f6c1317-0a73-4cb1-b991-6ac64732677c"),
                    UnitPrice = 100.00m,
                    Quantity = 1,
                },

                new CreateOrderItemCommand
                {
                    ProductId = new Guid("f60b5f70-cf37-49e4-92f3-a3d688839fcb"),
                    UnitPrice = 45.00m,
                    Quantity = 5
                }
            ]
        };
        return createCommand;
    }
}