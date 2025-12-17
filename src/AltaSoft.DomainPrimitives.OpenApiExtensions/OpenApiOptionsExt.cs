using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace AltaSoft.DomainPrimitives.OpenApiExtensions;

/// <summary>
/// Extensions for Microsoft.AspNetCore.OpenApi.OpenApiOptions to add schema transformer for all DomainPrimitive types.
/// </summary>
public static class OpenApiOptionsExt
{
    /// <summary>
    /// Extensions for Microsoft.AspNetCore.OpenApi.OpenApiOptions to add schema transformer for all DomainPrimitive types.
    /// </summary>
    /// <param name="options"></param>
    extension(OpenApiOptions options)
    {
        /// <summary>
        /// Registers a schema transformer that applies OpenApi helper schemas for all domain primitives discovered in assemblies.
        /// </summary>
        public OpenApiOptions AddDomainPrimitivesOpenApiSchemaTransformer()
        {
            options.AddSchemaTransformer(new DomainPrimitiveOpenApiSchemaTransformer());
            return options;
        }

    }

    /// <summary>
    /// Applies the domain primitive schemas to the specified OpenAPI schema.
    /// </summary>
    /// <param name="schemaToApply"></param>
    extension(OpenApiSchema schemaToApply)
    {
        internal void ApplyDomainPrimitiveSchemas(OpenApiSchema domainPrimitiveSchema, bool isNullable)
        {
            schemaToApply.Type = domainPrimitiveSchema.Type;
            schemaToApply.Format = domainPrimitiveSchema.Format;
            schemaToApply.Properties = domainPrimitiveSchema.Properties;
            schemaToApply.Required = domainPrimitiveSchema.Required;
            schemaToApply.Description = domainPrimitiveSchema.Description;
            schemaToApply.Example = domainPrimitiveSchema.Example;
            schemaToApply.Enum = domainPrimitiveSchema.Enum;
            schemaToApply.MinLength = domainPrimitiveSchema.MinLength;
            schemaToApply.MaxLength = domainPrimitiveSchema.MaxLength;
            schemaToApply.Minimum = domainPrimitiveSchema.Minimum;
            schemaToApply.Maximum = domainPrimitiveSchema.Maximum;
            schemaToApply.Pattern = domainPrimitiveSchema.Pattern;
            if (schemaToApply.Metadata is not null)
            {
                //that's how it is filled
                isNullable |= schemaToApply.Metadata.TryGetValue("x-is-nullable-property", out var nullable) && nullable is true;
                schemaToApply.Metadata.Clear();
                schemaToApply.Metadata.Add("x-schema-id", "");
            }

            if (isNullable)
                schemaToApply.Type |= JsonSchemaType.Null;
        }
    }
}
