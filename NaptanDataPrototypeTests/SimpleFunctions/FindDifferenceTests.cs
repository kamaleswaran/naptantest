using FluentAssertions;
using NaptanDataPrototype.SimpleFunctions;
using Xunit;

namespace NaptanDataPrototypeTests.SimpleFunctions;

public class FindDifferenceTests
{
    [Theory]
    [InlineData(51.3202179, 51.32022, 0.00000)]
    [InlineData(51.32000, 51.32001, 0.00001)]
    [InlineData(51.32021, 51.32031, 0.00010)]
    [InlineData(56.973343, 56.973370000000003, 0.00003)]
    public void ShouldGetDifferenceValue(double val1, double val2, double expectedResult)
    {
        var result = Function.FindDifference(val1, val2);

        result.Should().Be(expectedResult);
    }
}