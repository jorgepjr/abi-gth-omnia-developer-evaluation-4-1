using Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.OrdersTests;

public class CreateOrderHandlerTests : IClassFixture<InMemoryDataBase>
{
    private readonly DefaultContext _context;
    private readonly CustomerRepository _customerRepository;
    private readonly OrderRepository _orderRepository;
    private readonly ProductRepository _productRepository;
    private readonly ShopRepository _shopRepository;
    private readonly ILogger<CreateOrderHandler> _logger;

    public CreateOrderHandlerTests(InMemoryDataBase fixture)
    {
        _context = fixture.Context;
        _customerRepository = new CustomerRepository(_context);
        _orderRepository = new OrderRepository(_context);
        _productRepository = new ProductRepository(_context);
        _shopRepository = new ShopRepository(_context);
        _logger = Substitute.For<ILogger<CreateOrderHandler>>();
    }

    [Fact(DisplayName = "register orders and add 4 order item")]
    public async Task MustCreateOrderWithFourItems()
    {
        //Arrange
        var productsPrices = new[] { 10.6m, 300m, 500m, 600m, 800m };
        await CreateProducts(productsPrices);
        
        var products = await _context.Products
            .Where(x=> productsPrices.Contains(x.Price))
            .ToListAsync();
        
        var customer = await _customerRepository.CreateAsync(new Customer { Name = "Jorge" });
        var shop = await _shopRepository.CreateAsync(new Shop { TradeName = "Teste-shop" });

        var command = new CreateOrderCommand
        {
            CustomerId = customer.Id,
            ShopId = shop.Id,
            OrderItems = CreateAndGetOrderItemsCommand(products, [1, 2, 3, 4]),
        };
        
        var handler = new CreateOrderHandler(_orderRepository, _productRepository, _logger);

        //Action
        var response = await handler.Handle(command, CancellationToken.None);

        //Asserts
        Assert.NotNull(response);
        Assert.Equal(shop.Id, response.ShopId);
        Assert.Equal(customer.Id, response.CustomerId);
        Assert.Equal(4, response.OrderItems.Count());
    }
    
    [Fact(DisplayName = "Compras acima de 4 itens idênticos têm 10% de desconto")]
    public async Task MustApplyTenPercentDiscountOnOrdersAboveFourIdenticalItems()
    {
        //Arrange
        var expectedResult = 45.0m;
        
        var unitPriceProduct = new[] {10.0m};
        await CreateProducts(unitPriceProduct);

        var products = await _context.Products
            .Where(x => unitPriceProduct.Contains(x.Price))
            .ToListAsync();
        
        var customer = await _customerRepository.CreateAsync(new Customer { Name = "Jorge" });
        var shop = await _shopRepository.CreateAsync(new Shop { TradeName = "Teste-shop" });

        var handler = new CreateOrderHandler(_orderRepository, _productRepository, _logger);
        
        //Action
        var response = await handler.Handle(new CreateOrderCommand
        {
            CustomerId = customer.Id,
            ShopId = shop.Id,
            OrderItems = CreateAndGetOrderItemsCommand(products, [5]),
        }, CancellationToken.None);
        
        //Assert
        Assert.Equal(expectedResult, response.Total);
    }
    
    [Fact(DisplayName = "Compras entre 10 e 20 itens idênticos têm 20% de desconto")]
    public async Task MustApplyTwentyPercentDiscountWhenOrderContainsBetweenTenAndTwentyIdenticalItems()
    {
        //Arrange
        var expectedResult = 408m;
            
        var unitPriceProduct = new[] {15.0m, 20m};
        await CreateProducts(unitPriceProduct);
        
        var products = await _context.Products
            .Where(x => unitPriceProduct.Contains(x.Price))
            .ToListAsync();
        var customer = await _customerRepository.CreateAsync(new Customer { Name = "Jorge" });
        var shop = await _shopRepository.CreateAsync(new Shop { TradeName = "Teste-shop" });

        var orderItems = CreateAndGetOrderItemsCommand(products, [18, 12]);
        var command = new CreateOrderCommand
        {
            CustomerId = customer.Id,
            ShopId = shop.Id,
            OrderItems = orderItems,
        };
        
        var handler = new CreateOrderHandler(_orderRepository, _productRepository, _logger);
            
        //Action
        var response = await handler.Handle(command, CancellationToken.None);
        
        //Assert
        Assert.Equal(expectedResult, response.Total);
    }

    private static List<CreateOrderItemCommand> CreateAndGetOrderItemsCommand(List<Product> products, int[] quantities)
    {
        List<CreateOrderItemCommand> orderItems = [];
        
        for (int index = 0; index < quantities.Length; index++)
        {
            var item = new CreateOrderItemCommand { ProductId = products[index].Id, Quantity = quantities[index] };
            orderItems.Add(item);
        }

        return orderItems;
    }

    private async Task CreateProducts(decimal[] prices)
    {
        for (int i = 0; i < prices.Length; i++)
        {
            await _productRepository.CreateAsync(new Product { Name = $"Item - {i}", Price = prices[i] });
        }
    }
}