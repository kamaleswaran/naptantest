using FluentAssertions;
using NaptanDataPrototype.Functions;
using Xunit;

namespace NaptanDataPrototypeTests;

public class FunctionsTests
{
    [Theory]
    [InlineData(10.00001, 10.00001)]
    [InlineData(51.3202179, 51.32022)]
    public void ShouldMatchWithDefaultAdjustDifference(double val1, double val2)
    {
        var result = Functions.IsMatching(val1, val2);

        result.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(10.0002, 10.0001)]
    [InlineData(10.0001, 10.0002)]
    [InlineData(51.3202179, 51.3201179)]
    public void ShouldNotMatchWithDefaultAdjustDifference(double val1, double val2)
    {
        var result = Functions.IsMatching(val1, val2);

        result.Should().BeFalse();
    }
    
    [Theory]
    [InlineData(10.00002, 10.00001, 0.0001)]
    [InlineData(50.80798, 50.80799, 0.0003)]
    [InlineData(-0.06384, -0.06387, 0.0003)]
    public void ShouldMatchWhenGreaterThanOrEqualToAcceptableDifference(double val1, double val2, float acceptedDifference)
    {
        var result = Functions.IsMatching(val1, val2, acceptedDifference);

        result.Should().BeTrue();
    }
    
    [Fact]
    public void ShouldNotMatchWhenAcceptableDifferenceBeenIsLess()
    {
        var val1 = 10.00050;
        var val2 = 10.00010;
        var result = Functions.IsMatching(val1, val2, 0.0001);

        result.Should().BeFalse();
    }
}