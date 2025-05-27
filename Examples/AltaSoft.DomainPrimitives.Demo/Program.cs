using AltaSoft.DomainPrimitives.Demo;
using AltaSoft.DomainPrimitives.SwaggerExtensions;
using DomainPrimitivesDemo;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.AddAllDomainPrimitivesSwaggerMappings());
builder.Services.AddSingleton<CustomerService>();
builder.Services.AddSingleton<TransferService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var customerGroup = app.MapGroup("v1/Customers")
    .WithTags("Customers")
    .WithOpenApi();

var transferGroup = app.MapGroup("v1/Transfers")
    .WithTags("Transfers")
    .WithOpenApi();

AddCustomerEndpoints(customerGroup);
AddTransferEndpoints(transferGroup);
app.UseHttpsRedirection();

app.Run();

void AddCustomerEndpoints(IEndpointRouteBuilder routeGroupBuilder)
{
    routeGroupBuilder.MapPost("Add", (Customer command, [FromServices] CustomerService customerService) => customerService.AddCustomerAsync(command));
    routeGroupBuilder.MapPost("SetAddress", (SetCustomerAddress command, [FromServices] CustomerService customerService) => customerService.SetCustomerAddressAsync(command));
    routeGroupBuilder.MapGet("{id}", (CustomerId id, [FromServices] CustomerService customerService) => customerService.GetCustomerByIdAsync(id));
}

void AddTransferEndpoints(IEndpointRouteBuilder routeGroupBuilder)
{
    routeGroupBuilder.MapPost("Add", (Transfer command, [FromServices] TransferService service) => service.AddTransferAsync(command));
    routeGroupBuilder.MapGet("{id}", (TransferId id, [FromServices] TransferService service) => service.GetTransferByIdAsync(id));
    routeGroupBuilder.MapGet("ListTotalAmounts", async (TransferService service) =>
    {
        var result = await service.ListAsync();
        return (PositiveAmount)result.Sum(x => x.TransferAmount + x.TransferFee ?? 0);
    });
}
