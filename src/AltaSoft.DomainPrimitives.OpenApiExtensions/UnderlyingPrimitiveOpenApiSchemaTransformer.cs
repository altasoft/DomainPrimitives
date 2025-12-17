using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace AltaSoft.DomainPrimitives.OpenApiExtensions;

/// <summary>
/// An OpenAPI schema transformer that maps DomainPrimitive types to their corresponding OpenAPI schemas. By using Reflection to determine the underlying primitive type.
/// <remarks>This should be used if OpenApiMappings are not generated from primitives</remarks>
/// </summary>
public sealed class UnderlyingPrimitiveOpenApiSchemaTransformer : IOpenApiSchemaTransformer
{
    /// <inheritdoc />
    public Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context, CancellationToken cancellationToken)
    {
        var type = context.JsonTypeInfo.Type;
        var isNullable = false;
        if (Nullable.GetUnderlyingType(type) is { } t)
        {
            type = t;
            isNullable = true;
        }
        if (!type.TryGetUnderlyingDomainPrimitiveType(out var primitiveType) || !s_simpleTypeToOpenApiSchema.TryGetValue(primitiveType, out var domainPrimitiveSchema))
            return Task.CompletedTask;

        schema.ApplyDomainPrimitiveSchemas(domainPrimitiveSchema, isNullable);

        return Task.CompletedTask;
    }

    /// <summary>
    /// copied from https://github.com/dotnet/aspnetcore/blob/main/src/OpenApi/src/Extensions/JsonNodeSchemaExtensions.cs#L27
    /// </summary>
    private static readonly FrozenDictionary<Type, OpenApiSchema> s_simpleTypeToOpenApiSchema = new Dictionary<Type, OpenApiSchema>
    {
        [typeof(bool)] = new() { Type = JsonSchemaType.Boolean },
        [typeof(byte)] = new() { Type = JsonSchemaType.Integer, Format = "uint8" },
        [typeof(int)] = new() { Type = JsonSchemaType.Integer, Format = "int32" },
        [typeof(uint)] = new() { Type = JsonSchemaType.Integer, Format = "uint32" },
        [typeof(long)] = new() { Type = JsonSchemaType.Integer, Format = "int64" },
        [typeof(ulong)] = new() { Type = JsonSchemaType.Integer, Format = "uint64" },
        [typeof(short)] = new() { Type = JsonSchemaType.Integer, Format = "int16" },
        [typeof(ushort)] = new() { Type = JsonSchemaType.Integer, Format = "uint16" },
        [typeof(float)] = new() { Type = JsonSchemaType.Number, Format = "float" },
        [typeof(double)] = new() { Type = JsonSchemaType.Number, Format = "double" },
        [typeof(decimal)] = new() { Type = JsonSchemaType.Number, Format = "double" },
        [typeof(DateTime)] = new() { Type = JsonSchemaType.String, Format = "date-time" },
        [typeof(DateTimeOffset)] = new() { Type = JsonSchemaType.String, Format = "date-time" },
        [typeof(Guid)] = new() { Type = JsonSchemaType.String, Format = "uuid" },
        [typeof(char)] = new() { Type = JsonSchemaType.String, Format = "char" },
        [typeof(string)] = new() { Type = JsonSchemaType.String },
        [typeof(TimeOnly)] = new() { Type = JsonSchemaType.String, Format = "time" },
        [typeof(DateOnly)] = new() { Type = JsonSchemaType.String, Format = "date" },
    }.ToFrozenDictionary();
}
