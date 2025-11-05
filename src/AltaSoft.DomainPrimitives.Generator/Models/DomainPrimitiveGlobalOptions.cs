namespace AltaSoft.DomainPrimitives.Generator.Models;

/// <summary>
/// Configuration options for controlling code generation of Domain Primitive types.
/// </summary>
internal sealed record DomainPrimitiveGlobalOptions
{
    // Behavior toggles
    /// <summary>
    /// Controls whether the generator emits implicit conversions between a domain primitive and its underlying CLR type.
    /// </summary>
    /// <remarks>
    /// MSBuild key: build_property.DomainPrimitiveGenerator_GenerateImplicitConversions (default: true).
    /// When false, no implicit operator to/from the underlying type is generated, reducing accidental conversions at API boundaries.
    /// Existing consumers are unaffected unless they opt out by setting the MSBuild property to false.
    /// </remarks>
    public bool GenerateImplicitConversions { get; set; } = true;

    /// <summary>
    /// Controls whether numeric operators are enabled by default for primitives backed by numeric types.
    /// </summary>
    /// <remarks>
    /// MSBuild key: build_property.DomainPrimitiveGenerator_DefaultNumericOperationsEnabled (default: true).
    /// When false, addition/subtraction/multiplication/division/modulus are not generated unless explicitly enabled via
    /// <c>[SupportedOperations]</c> on the type. This helps prevent accidental arithmetic on domain values.
    /// </remarks>
    public bool DefaultNumericOperationsEnabled { get; set; } = true;

    /// <summary>
    /// Enables safe semantics for default(struct) domain primitives.
    /// </summary>
    /// <remarks>
    /// MSBuild key: build_property.DomainPrimitiveGenerator_SafeDefaultStructSemantics (default: false).
    /// When true: default == default, default.GetHashCode() == 0, and comparisons treat default as less than initialized.
    /// Kept off by default for backward compatibility; opt in per project to standardize safer equality/hash.
    /// </remarks>
    public bool SafeDefaultStructSemantics { get; set; }

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

    /// <summary>
    /// Gets or sets a value indicating whether Entity Framework Core value converters should be generated for Domain Primitive types.
    /// </summary>
    /// <value>
    ///   <c>true</c> if Entity Framework Core value converters should be generated; otherwise, <c>false</c>.
    /// </value>
    public bool GenerateEntityFrameworkCoreValueConverters { get; set; }
}
