namespace AltaSoft.DomainPrimitives.Generator.Models;

/// <summary>
/// Configuration options for controlling code generation of Domain Primitive types.
/// </summary>
internal sealed record DomainPrimitiveGlobalOptions
{
    /// <summary>
    /// Gets or sets a value indicating whether to generate JSON converters for Domain Primitive types.
    /// The default value is true.
    /// </summary>
    /// <value>
    ///   <c>true</c> if JSON converters should be generated; otherwise, <c>false</c>.
    /// </value>
    public bool GenerateJsonConverters { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether type converters should be generated for Domain Primitive types.
    /// The default value is true.
    /// </summary>
    /// <value>
    ///   <c>true</c> if type converters should be generated; otherwise, <c>false</c>.
    /// </value>
    public bool GenerateTypeConverters { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether to generate OpenAPI helper for Domain Primitive types.
    /// The default value is true.
    /// </summary>
    /// <value>
    ///   <c>true</c> if OpenApiHelper should be generated; otherwise, <c>false</c>.
    /// </value>
    public bool GenerateOpenApiHelper { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether XML serialization should be generated for Domain Primitive types.
    /// The default value is false.
    /// </summary>
    /// <value>
    ///   <c>true</c> if XML serialization methods should be generated; otherwise, <c>false</c>.
    /// </value>
    public bool GenerateXmlSerialization { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether Entity Framework Core value converters should be generated for Domain Primitive types.
    /// </summary>
    /// <value>
    ///   <c>true</c> if Entity Framework Core value converters should be generated; otherwise, <c>false</c>.
    /// </value>
    public bool GenerateEntityFrameworkCoreValueConverters { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether Implicit operators should be generated for Domain Primitive types.
    /// </summary>
    /// <value>
    ///   <c>true</c> if Implicit operators should be generated; otherwise, <c>false</c>.
    /// </value>
    public bool GenerateImplicitOperators { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether Numeric operators should be generated for Domain Primitive types.
    /// </summary>
    /// <value>
    ///   <c>true</c> if Numeric operators should be generated; otherwise, <c>false</c>.
    /// </value>
    public bool GenerateNumericOperators { get; set; } = true;
}
