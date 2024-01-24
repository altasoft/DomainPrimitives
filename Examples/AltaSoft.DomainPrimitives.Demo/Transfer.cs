using DomainPrimitivesDemo;

namespace AltaSoft.DomainPrimitives.Demo;

public sealed record Transfer(PositiveAmount TransferAmount, Fee? TransferFee, CustomerId From, CustomerId To, Iban FromIban, Iban? ToIban);