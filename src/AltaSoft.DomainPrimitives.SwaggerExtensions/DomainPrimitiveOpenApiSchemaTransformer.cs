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
    private readonly Lazy<FrozenDictionary<Type, OpenApiSchema>> _schemas = new(Initialize);
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
        if (!_schemas.Value.TryGetValue(context.JsonTypeInfo.Type, out var domainPrimitiveSchema))
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

        return Task.CompletedTask;
    }
}
#endif
