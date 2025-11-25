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
    private static readonly Lazy<FrozenDictionary<Type, OpenApiSchema>> s_schemas = new(Initialize);
    private static FrozenDictionary<Type, OpenApiSchema> Initialize()
    {
        var result = new Dictionary<Type, OpenApiSchema>();
        OpenApiHelperProcessor.ProcessOpenApiHelpers(values =>
        {
            foreach (var (k, v) in values) result.TryAdd(k, v);
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
        if (!s_schemas.Value.TryGetValue(type, out var domainPrimitiveSchema))
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

        if (context.JsonPropertyInfo is null)
            return Task.CompletedTask;

        if (isNullable)
            schema.Type |= JsonSchemaType.Null;

        return Task.CompletedTask;
    }
}
#endif
