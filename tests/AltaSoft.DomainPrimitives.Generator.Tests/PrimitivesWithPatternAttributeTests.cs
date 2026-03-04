namespace AltaSoft.DomainPrimitives.Generator.Tests;

public class PrimitivesWithPatternAttributeTests
{
    [Theory]
    [InlineData("[A-Z]{3}")]
    [InlineData("[A-Z]{10}")]
    [InlineData("\\d+")]
    [InlineData("\\w+")]
    [InlineData("\\s+")]
    [InlineData("[A-Z]{3}\\d{2}")]
    [InlineData("[A-Z]{3}-\\d{3}")]
    [InlineData("^\\d{4}-\\d{2}-\\d{2}$")]
    [InlineData("^\\w+@\\w+\\.\\w+$")]
    [InlineData("^\\d{3}-\\d{2}-\\d{4}$")]
    public void Pattern_Should_Compile(string pattern)
    {
        var source = $$"""
                       using System;
                       using System.Collections.Generic;
                       using System.Linq;
                       using System.Text;
                       using System.Threading.Tasks;
                       using AltaSoft.DomainPrimitives;

                       namespace AltaSoft.DomainPrimitives;

                       /// <summary>
                       /// A string domain primitive with both length and pattern validation attributes, as well as a custom validation method.
                       /// </summary>
                       [StringLength(1, 100)]
                       [Pattern(@"{{pattern}}, true")]
                       internal partial class StringWithLengthAndPattern : IDomainValue<string>
                       {
                           /// <inheritdoc/>
                           public static PrimitiveValidationResult Validate(string value)
                           {
                               return PrimitiveValidationResult.Ok;
                           }
                       }

                       """;

        TestHelper.Compile(source, (_, sources, _) => Assert.Equal(4, sources.Count));

    }

}
