using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.UpdateOrder;

public class UpdateOrderRequest
{
    [JsonIgnore]
    public Guid OrderId { get; set; }
    
    public List<UpdateOrderItemRequest> OrderItems { get; set; } = [];
}