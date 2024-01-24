using DomainPrimitivesDemo;

namespace AltaSoft.DomainPrimitives.Demo;
public sealed record Customer(CustomerId CustomerId, CustomerName CustomerName, CustomerAddress CustomerAddress);
public sealed record SetCustomerAddress(CustomerId CustomerId, CustomerAddress CustomerAddress);