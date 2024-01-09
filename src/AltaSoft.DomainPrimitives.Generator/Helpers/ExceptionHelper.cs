using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AltaSoft.DomainPrimitives.Generator.Helpers;

/// <summary>
/// Helper class for verifying exceptions in methods.
/// </summary>
internal static class ExceptionHelper
{
	/// <summary>
	/// Verifies that exceptions thrown within a method are of the correct type.
	/// </summary>
	/// <param name="method">The method symbol to verify.</param>
	/// <param name="context">The source production context.</param>
	internal static void VerifyException(IMethodSymbol method, SourceProductionContext context)
	{
		// Retrieve the method's syntax reference.
		var syntaxRef = method.DeclaringSyntaxReferences.FirstOrDefault();

		if (syntaxRef?.GetSyntax() is not MethodDeclarationSyntax methodSyntax)
			return;

		// Find all invalid exception types within the method.
		var invalidExceptionTypes = methodSyntax.DescendantNodes().OfType<ThrowStatementSyntax>()
			.Where(IsInvalidException)
			.ToList();

		if (invalidExceptionTypes.Count == 0)
			return;

		// Check for invalid exception types within catch blocks.
		foreach (var nonDomainValueException in invalidExceptionTypes)
		{
			var tryStatement = nonDomainValueException.FirstAncestorOrSelf<TryStatementSyntax>();
			if (tryStatement is null)
			{
				context.ReportDiagnostic(DiagnosticHelper.InvalidExceptionTypeThrown(nonDomainValueException.GetLocation()));
				continue;
			}

			var catchBlocks = tryStatement.Catches
				.SelectMany(x => x.Block.DescendantNodes().OfType<ThrowStatementSyntax>()).Where(IsInvalidException);

			ReportIncorrectExceptions(context, catchBlocks);
		}
	}

	/// <summary>
	/// Checks if the exception thrown in a <c>throw</c> statement is of the correct type.
	/// </summary>
	/// <param name="throwStatement">The <c>throw</c> statement syntax.</param>
	/// <returns><c>true</c> if the exception type is invalid; otherwise, <c>false</c>.</returns>
	private static bool IsInvalidException(ThrowStatementSyntax throwStatement)
	{
		if (throwStatement.Expression is ObjectCreationExpressionSyntax { Type: IdentifierNameSyntax identifierNameSyntax })
		{
			return identifierNameSyntax.Identifier.ValueText != "InvalidDomainValueException";
		}

		return true;
	}

	/// <summary>
	/// Reports incorrect exceptions in catch blocks.
	/// </summary>
	/// <param name="context">The source production context.</param>
	/// <param name="incorrectCatchStatements">The list of incorrect catch statements.</param>
	private static void ReportIncorrectExceptions(SourceProductionContext context, IEnumerable<ThrowStatementSyntax> incorrectCatchStatements)
	{
		foreach (var incorrectCatchStatement in incorrectCatchStatements)
		{
			context.ReportDiagnostic(DiagnosticHelper.InvalidExceptionTypeThrown(incorrectCatchStatement.GetLocation()));
		}
	}
}