namespace AltaSoft.DomainPrimitives.Generator.Models;

/// <summary>
/// Configuration options for controlling code generation of Domain Primitive types.
/// </summary>
internal sealed class DomainPrimitiveGlobalOptions
{
	/// <summary>
	/// Gets or sets a value indicating whether to generate JSON converters for Domain Primitive types.
	/// </summary>
	public bool GenerateJsonConverters { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether to generate type converters for Domain Primitive types.
	/// </summary>
	public bool GenerateTypeConverters { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether to generate Swagger converters for Domain Primitive types.
	/// </summary>
	public bool GenerateSwaggerConverters { get; set; }
}