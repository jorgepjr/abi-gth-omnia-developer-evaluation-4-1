using System.Net;
using System.Net.Http.Json;
using Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder;
using Xunit;

namespace Ambev.DeveloperEvaluation.Functional;

public class OrderFunctionalTests(WebAppFactory factory) : IClassFixture<WebAppFactory>
{
    private const string Uri = "api/orders";
    
    private readonly HttpClient _client = factory.CreateClient();

    [Fact(DisplayName = ("Retorna BadRequest no envio de dados vazios de pedidos"))]
    public async Task ShouldReturnBadRequestWhenOrderDataIsEmpty()
    {
        var response = await _client.PostAsJsonAsync("/api/orders", new CreateOrderRequest{});

        var res = response.StatusCode;
        Assert.Equal(HttpStatusCode.BadRequest,response.StatusCode);
    }
}