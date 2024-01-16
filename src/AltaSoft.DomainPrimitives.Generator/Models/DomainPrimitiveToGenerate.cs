using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AltaSoft.DomainPrimitives.Generator.Models;

internal readonly record struct DomainPrimitiveToGenerate
{
	public DomainPrimitiveToGenerate(TypeDeclarationSyntax typeSyntax, INamedTypeSymbol typeSymbol)
	{
		TypeSyntax = typeSyntax;
		TypeSymbol = typeSymbol;
	}

	public TypeDeclarationSyntax TypeSyntax { get; }
	public INamedTypeSymbol TypeSymbol { get; }
}