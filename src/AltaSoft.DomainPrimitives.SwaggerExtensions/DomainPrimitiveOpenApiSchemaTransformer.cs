#if NET10_0_OR_GREATER
using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace AltaSoft.DomainPrimitives.SwaggerExtensions;

/// <summary>
/// An OpenAPI schema transformer that maps DomainPrimitive types to their corresponding OpenAPI schemas.
/// </summary>
internal sealed class DomainPrimitiveOpenApiSchemaTransformer : IOpenApiSchemaTransformer
{
    private static readonly Lazy<FrozenDictionary<Type, OpenApiSchema>> s_primitiveSchemas = new(Initialize);
    private static FrozenDictionary<Type, OpenApiSchema> Initialize()
    {
        var result = new Dictionary<Type, OpenApiSchema>();
        OpenApiHelperProcessor.ProcessOpenApiHelpers(values =>
        {
            foreach (var (k, v) in values)
                result.TryAdd(k, v);
        });

        return result.ToFrozenDictionary();
    }
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
        if (!s_primitiveSchemas.Value.TryGetValue(type, out var domainPrimitiveSchema))
            return Task.CompletedTask;

        schema.Type = domainPrimitiveSchema.Type;
        schema.Format = domainPrimitiveSchema.Format;
        schema.Properties = domainPrimitiveSchema.Properties;
        schema.Required = domainPrimitiveSchema.Required;
        schema.Description = domainPrimitiveSchema.Description;
        schema.Example = domainPrimitiveSchema.Example;
        schema.Enum = domainPrimitiveSchema.Enum;
        schema.MinLength = domainPrimitiveSchema.MinLength;
        schema.MaxLength = domainPrimitiveSchema.MaxLength;
        schema.Minimum = domainPrimitiveSchema.Minimum;
        schema.Maximum = domainPrimitiveSchema.Maximum;
        schema.Pattern = domainPrimitiveSchema.Pattern;
        if (schema.Metadata is not null)
        {
            //that's how it is filled
            isNullable |= schema.Metadata.TryGetValue("x-is-nullable-property", out var nullable) && nullable is true;
            schema.Metadata.Clear();
            schema.Metadata.Add("x-schema-id", "");
        }

        if (isNullable)
            schema.Type |= JsonSchemaType.Null;

        return Task.CompletedTask;
    }
}

/// <summary>
/// An OpenAPI schema transformer that maps DomainPrimitive types to their corresponding OpenAPI schemas. By using Reflection to determine the underlying primitive type.
/// <remarks>This should be used if OpenApiMappings are not generated from primitives and <see cref="DomainPrimitiveOpenApiSchemaTransformer"/> is not used</remarks>
/// </summary>
public sealed class DomainPrimitiveOpenApiReflectionTransformer : IOpenApiSchemaTransformer
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

        schema.Type = domainPrimitiveSchema.Type;
        schema.Format = domainPrimitiveSchema.Format;
        schema.Properties = domainPrimitiveSchema.Properties;
        schema.Required = domainPrimitiveSchema.Required;
        schema.Description = domainPrimitiveSchema.Description;
        schema.Example = domainPrimitiveSchema.Example;
        schema.Enum = domainPrimitiveSchema.Enum;
        schema.MinLength = domainPrimitiveSchema.MinLength;
        schema.MaxLength = domainPrimitiveSchema.MaxLength;
        schema.Minimum = domainPrimitiveSchema.Minimum;
        schema.Maximum = domainPrimitiveSchema.Maximum;
        schema.Pattern = domainPrimitiveSchema.Pattern;
        if (schema.Metadata is not null)
        {
            //that's how it is filled
            isNullable |= schema.Metadata.TryGetValue("x-is-nullable-property", out var nullable) && nullable is true;
            schema.Metadata.Clear();
            schema.Metadata.Add("x-schema-id", "");
        }

        if (isNullable)
            schema.Type |= JsonSchemaType.Null;

        return Task.CompletedTask;
    }

    /// <summary>
    /// copied from https://github.com/dotnet/aspnetcore/blob/main/src/OpenApi/src/Extensions/JsonNodeSchemaExtensions.cs#L27
    /// </summary>
    private static readonly FrozenDictionary<Type, OpenApiSchema> s_simpleTypeToOpenApiSchema = new Dictionary<Type, OpenApiSchema>()
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
#endif
