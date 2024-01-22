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
    /// Gets or sets a value indicating whether to generate Swagger converters for Domain Primitive types.
    /// The default value is true.
    /// </summary>
    /// <value>
    ///   <c>true</c> if Swagger converters should be generated; otherwise, <c>false</c>.
    /// </value>
    public bool GenerateSwaggerConverters { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether XML serialization should be generated for Domain Primitive types.
    /// The default value is false.
    /// </summary>
    /// <value>
    ///   <c>true</c> if XML serialization methods should be generated; otherwise, <c>false</c>.
    /// </value>
    public bool GenerateXmlSerialization { get; set; }
}