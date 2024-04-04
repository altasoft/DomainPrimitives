﻿//HintName: SwaggerTypeHelper.g.cs
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by 'AltaSoft DomainPrimitives Generator'.
//     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#nullable enable

using AltaSoft.DomainPrimitives;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;

[assembly: AltaSoft.DomainPrimitives.DomainPrimitiveAssemblyAttribute]
namespace generator_Test.Converters.Extensions;

/// <summary>
/// Helper class providing methods to configure Swagger mappings for DomainPrimitive types of generator_Test
/// </summary>
public static class SwaggerTypeHelper
{
    /// <summary>
    /// Adds Swagger mappings for specific custom types to ensure proper OpenAPI documentation generation.
    /// </summary>
    /// <param name="options">The SwaggerGenOptions instance to which mappings are added.</param>
    /// <remarks>
    /// The method adds Swagger mappings for the following types:
    /// <see cref="DateTimeOffsetValue" />
    /// </remarks>
    public static void AddSwaggerMappings(this SwaggerGenOptions options)
    {
        options.MapType<DateTimeOffsetValue>(() => new OpenApiSchema
        {
            Type = "string",
            Format = "date-time",
            Title = "DateTimeOffsetValue",
            Default = new OpenApiDateTime(((DateTimeOffset)DateTimeOffsetValue.Default).DateTime)
        });
        options.MapType<DateTimeOffsetValue?>(() => new OpenApiSchema
        {
            Type = "string",
            Format = "date-time",
            Nullable = true,
            Title = "Nullable<DateTimeOffsetValue>",
            Default = new OpenApiDateTime(((DateTimeOffset)DateTimeOffsetValue.Default).DateTime)
        });
    }
}
