using DomainPrimitivesDemo;
using System.Collections.Concurrent;

namespace AltaSoft.DomainPrimitives.Demo;

public sealed class CustomerService
{
    private readonly ConcurrentDictionary<CustomerId, Customer> _customers = new();

    public Task<Customer?> GetCustomerByIdAsync(CustomerId customerId)
    {
        return Task.FromResult(_customers.GetValueOrDefault(customerId));
    }

    public Task AddCustomerAsync(Customer customer)
    {
        if (!_customers.TryAdd(customer.CustomerId, customer))
            throw new BadHttpRequestException("Customer already exists");

        return Task.CompletedTask;
    }

    public Task SetCustomerAddressAsync(SetCustomerAddress command)
    {
        if (!_customers.TryGetValue(command.CustomerId, out var customer))
            throw new BadHttpRequestException("Customer already exists");

        customer = customer with { CustomerAddress = command.CustomerAddress };

        _customers[command.CustomerId] = customer;
        return Task.CompletedTask;
    }
}
