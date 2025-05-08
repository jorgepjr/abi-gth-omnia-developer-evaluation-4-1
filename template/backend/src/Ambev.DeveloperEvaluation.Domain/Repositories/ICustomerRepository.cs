using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ICustomerRepository
{
    Task<Customer> CreateAsync(Customer customer, CancellationToken cancellationToken = default);
}