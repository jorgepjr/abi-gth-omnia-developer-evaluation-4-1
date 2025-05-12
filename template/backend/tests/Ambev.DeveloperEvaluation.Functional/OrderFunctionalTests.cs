using System.Net.Http.Json;
using Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

namespace Ambev.DeveloperEvaluation.Functional;

public class OrderFunctionalTests : IClassFixture<WebAppFactory>
{
    private readonly HttpClient _client;

    public OrderFunctionalTests(WebAppFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task MustCreateOrder()
    {
        var response = await _client.PostAsJsonAsync("/api/orders", new CreateOrderRequest{});
    }
}