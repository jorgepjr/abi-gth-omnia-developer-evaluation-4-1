using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Shop : BaseEntity
{
    public string TradeName { get; set; } = string.Empty;
}