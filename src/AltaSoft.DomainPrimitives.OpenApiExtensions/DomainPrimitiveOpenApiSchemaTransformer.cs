using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace AltaSoft.DomainPrimitives.OpenApiExtensions;

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

        schema.ApplyDomainPrimitiveSchemas(domainPrimitiveSchema, isNullable);

        return Task.CompletedTask;
    }

}
