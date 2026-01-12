using System.ComponentModel.DataAnnotations;
using DomainPrimitivesDemo;

namespace AltaSoft.DomainPrimitives.Demo;
// ReSharper disable InconsistentNaming
public sealed record Customer(

    [Required] CustomerId A_CustomerId, [Required] Guid A_CustomerIdDotNet,
    [Required] BirthDate B_BirthDate, [Required] DateOnly B_BirthDateDotNet,
    [Required] CustomerName C_CustomerName, [Required] string C_CustomerNameDotNet,
    [Required] PositiveAmount D_Amount, [Required] decimal D_AmountDotnet)
{
    public CustomerAddress? CustomerAddress { get; set; } //ignore
}

public sealed record CustomerNullable(
    CustomerId? A_CustomerId,
    Guid? A_CustomerIdDotNet,
    BirthDate? B_BirthDate,
    DateOnly? B_BirthDateDotNet,
    CustomerName? C_CustomerName,
    string? C_CustomerNameDotNet,
    PositiveAmount? D_Amount,
    decimal? D_AmountDotnet);

// ReSharper restore InconsistentNaming
public sealed record SetCustomerAddress(CustomerId CustomerId, CustomerAddress CustomerAddress);
public sealed record SetCustomerAddressNullable(CustomerId CustomerId, CustomerAddress? CustomerAddress, PositiveAmount? Amount);

