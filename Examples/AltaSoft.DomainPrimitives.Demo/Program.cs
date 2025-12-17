using AltaSoft.DomainPrimitives.Demo;
using AltaSoft.DomainPrimitives.OpenApiExtensions;
using AltaSoft.DomainPrimitives.SwaggerExtensions;
using DomainPrimitivesDemo;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi(options =>
{
    options.AddDomainPrimitivesOpenApiSchemaTransformer();

    //options.AddSchemaTransformer(new DomainPrimitiveOpenApiReflectionTransformer());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => { options.AddAllDomainPrimitivesSwaggerMappings(); });
builder.Services.AddSingleton<CustomerService>();
builder.Services.AddSingleton<TransferService>();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

//scalar
app.MapOpenApi();
app.MapScalarApiReference(options =>
{
    options.Theme = ScalarTheme.DeepSpace;
});

var customerGroup = app.MapGroup("v1/Customers").WithTags("Customers");
var transferGroup = app.MapGroup("v1/Transfers").WithTags("Transfers");

AddCustomerEndpoints(customerGroup);
AddTransferEndpoints(transferGroup);
app.UseHttpsRedirection();

app.Run();

static void AddCustomerEndpoints(IEndpointRouteBuilder routeGroupBuilder)
{
    routeGroupBuilder.MapPost("Add",
        (Customer command, [FromServices] CustomerService customerService) => customerService.AddCustomerAsync(command));

    routeGroupBuilder.MapPost("AddNull",
        (CustomerNullable command, [FromServices] CustomerService customerService) => "1");

    routeGroupBuilder.MapPost("SetAddress",
        (SetCustomerAddress command, [FromServices] CustomerService customerService) => customerService.SetCustomerAddressAsync(command));
    routeGroupBuilder.MapPost("SetAddressNullable",
        (SetCustomerAddressNullable command, [FromServices] CustomerService customerService) =>
            customerService.SetCustomerAddressNullableAsync(command));
    routeGroupBuilder.MapGet("{id}", (CustomerId id, [FromServices] CustomerService customerService) => customerService.GetCustomerByIdAsync(id));
}

static void AddTransferEndpoints(IEndpointRouteBuilder routeGroupBuilder)
{
    routeGroupBuilder.MapPost("Add", (Transfer command, [FromServices] TransferService service) => service.AddTransferAsync(command));
    routeGroupBuilder.MapGet("{id}", (TransferId id, [FromServices] TransferService service) => service.GetTransferByIdAsync(id));
    routeGroupBuilder.MapGet("filterValues",
        ([FromQuery] TransferId? id, [FromServices] TransferService service) =>
            id is null ? Task.FromResult<Transfer?>(null) : service.GetTransferByIdAsync(id.Value));
    routeGroupBuilder.MapGet("ListTotalAmounts", async (TransferService service) =>
    {
        var result = await service.ListAsync();
        return (PositiveAmount)result.Sum(x => x.TransferAmount + x.TransferFee ?? 0);
    });
}

