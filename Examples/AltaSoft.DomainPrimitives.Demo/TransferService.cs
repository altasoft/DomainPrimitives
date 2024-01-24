using DomainPrimitivesDemo;
using System.Collections.Concurrent;

#pragma warning disable IDE0290

namespace AltaSoft.DomainPrimitives.Demo;

public sealed class TransferService
{
    private readonly ConcurrentDictionary<TransferId, Transfer> _transfers = new();
    private static long s_value = 1;
    private readonly CustomerService _service;

    public TransferService(CustomerService service)
    {
        _service = service;
    }

    public Task<Transfer?> GetTransferByIdAsync(TransferId transferId)
    {
        return Task.FromResult(_transfers.GetValueOrDefault(transferId));
    }

    public Task<List<Transfer>> ListAsync() => Task.FromResult(_transfers.Values.ToList());

    public async Task<TransferId> AddTransferAsync(Transfer transfer)
    {
        if (await _service.GetCustomerByIdAsync(transfer.From) is null)
            throw new BadHttpRequestException("From customer cannot be found");

        if (await _service.GetCustomerByIdAsync(transfer.To) is null)
            throw new BadHttpRequestException("To customer cannot be found");

        Interlocked.Increment(ref s_value);
        var transferId = new TransferId(s_value);

        if (!_transfers.TryAdd(transferId, transfer))
            throw new BadHttpRequestException("Customer already exists");

        return transferId;
    }
}
