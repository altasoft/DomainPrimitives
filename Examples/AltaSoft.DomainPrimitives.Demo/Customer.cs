using DomainPrimitivesDemo;

namespace AltaSoft.DomainPrimitives.Demo;

public sealed record Customer(CustomerId CustomerId, BirthDate BirthDate, CustomerName CustomerName, CustomerAddress CustomerAddress);
public sealed record SetCustomerAddress(CustomerId CustomerId, CustomerAddress CustomerAddress);
public sealed record SetCustomerAddressNullable(CustomerId CustomerId, CustomerAddress? CustomerAddress, PositiveAmount? Amount);
