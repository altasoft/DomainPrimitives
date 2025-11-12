
using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DomainPrimitivesDemo.Converters.Extensions;

/// <summary>
/// Helper class providing methods to configure Swagger mappings for DomainPrimitive types of DomainPrimitivesDemo
/// </summary>
public static class SwaggerTypeHelper2
{
    /// <summary>
    /// Mapping of DomainPrimitive types to OpenApiSchema definitions.
    /// </summary>
    public static FrozenDictionary<Type, OpenApiSchema> Mapping = new Dictionary<Type, OpenApiSchema>
    {
        {
            typeof(PositiveAmount),
            new OpenApiSchema
            {
                Type = JsonSchemaType.Number,
                Format = "decimal",
                Title = "PositiveAmount",
                Description = "Positive amount",
                Example = JsonValue.Create(10.5)
            }
        }
    }.ToFrozenDictionary();
    /// <summary>
    /// Adds Swagger mappings for specific custom types to ensure proper OpenAPI documentation generation.
    /// </summary>
    /// <param name="options">The SwaggerGenOptions instance to which mappings are added.</param>
    /// <remarks>
    /// The method adds Swagger mappings for the following types:
    /// <see cref="PositiveAmount" />
    /// <see cref="CustomerId" />
    /// <see cref="TransferId" />
    /// <see cref="CustomerName" />
    /// <see cref="TotalAmount" />
    /// <see cref="Iban" />
    /// <see cref="CustomerAddress" />
    /// <see cref="BirthDate" />
    /// <see cref="Fee" />
    /// <see cref="Currency" />
    /// </remarks>
    public static void AddSwaggerMappings(this SwaggerGenOptions options)
    {
        options.MapType<PositiveAmount>(() => new OpenApiSchema
        {
            Type = JsonSchemaType.Number,
            Format = "decimal",
            Title = "PositiveAmount",
            Description = "Positive amount",
            Example = JsonValue.Create(10.5)
        });
        options.MapType<PositiveAmount>(() => new OpenApiSchema
        {
            Type = JsonSchemaType.Number | JsonSchemaType.Null,
            Format = "decimal",
            Title = "PositiveAmount",
            Description = @"Positive amount",
            Example = JsonValue.Create(10.5)
        });
        options.MapType<CustomerId>(() => new OpenApiSchema
        {
            Type = JsonSchemaType.String,
            Format = "uuid",
            Title = "CustomerId",
            Description = @"CustomerId",
            Example = JsonValue.Create("608eadda-6730-4031-9333-8a21e40210ed")
        });
        options.MapType<CustomerId>(() => new OpenApiSchema
        {
            Type = JsonSchemaType.String | JsonSchemaType.Null,
            Format = "uuid",
            Title = "CustomerId",
            Description = @"CustomerId",
            Example = JsonValue.Create("608eadda-6730-4031-9333-8a21e40210ed")
        });
        options.MapType<TransferId>(() => new OpenApiSchema
        {
            Type = JsonSchemaType.Integer,
            Format = "int64",
            Title = "TransferId",
            Description = @"CustomerId",
            Example = JsonValue.Create(132)
        });
        options.MapType<TransferId>(() => new OpenApiSchema
        {
            Type = JsonSchemaType.Integer | JsonSchemaType.Null,
            Format = "int64",
            Title = "TransferId",
            Description = @"CustomerId",
            Example = JsonValue.Create(132)
        });
        options.MapType<CustomerName>(() => new OpenApiSchema
        {
            Type = JsonSchemaType.String,
            Title = "CustomerName",
            Description = @"Customer Name",
            Example = JsonValue.Create("Test Name")
        });
        options.MapType<TotalAmount>(() => new OpenApiSchema
        {
            Type = JsonSchemaType.Number,
            Format = "decimal",
            Title = "TotalAmount",
            Description = @"Total amount",
            Example = JsonValue.Create(10.5)
        });
        options.MapType<TotalAmount>(() => new OpenApiSchema
        {
            Type = JsonSchemaType.Number | JsonSchemaType.Null,
            Format = "decimal",
            Title = "TotalAmount",
            Description = @"Total amount",
            Example = JsonValue.Create(10.5)
        });
        options.MapType<Iban>(() => new OpenApiSchema
        {
            Type = JsonSchemaType.String,
            Title = "Iban",
            Description = @"Iban",
            Example = JsonValue.Create("GB82WEST12345698765432")
        });
        options.MapType<CustomerAddress>(() => new OpenApiSchema
        {
            Type = JsonSchemaType.String,
            Title = "CustomerAddress",
            Description = @"Customer Address",
            Example = JsonValue.Create("Customer address N35 apt 14")
        });
        options.MapType<BirthDate>(() => new OpenApiSchema
        {
            Type = JsonSchemaType.String,
            Format = "date",
            Title = "BirthDate",
            Description = @"CustomerId"
        });
        options.MapType<BirthDate>(() => new OpenApiSchema
        {
            Type = JsonSchemaType.String | JsonSchemaType.Null,
            Format = "date",
            Title = "BirthDate",
            Description = @"CustomerId"
        });
        options.MapType<Fee>(() => new OpenApiSchema
        {
            Type = JsonSchemaType.Number,
            Format = "decimal",
            Title = "Fee",
            Description = @"Transfer Fees",
            Example = JsonValue.Create(1.15)
        });
        options.MapType<Fee>(() => new OpenApiSchema
        {
            Type = JsonSchemaType.Number | JsonSchemaType.Null,
            Format = "decimal",
            Title = "Fee",
            Description = @"Transfer Fees",
            Example = JsonValue.Create(1.15)
        });
        options.MapType<Currency>(() => new OpenApiSchema
        {
            Type = JsonSchemaType.String,
            Title = "Currency",
            Description = @"Transfer Currency"
        });
    }
}
