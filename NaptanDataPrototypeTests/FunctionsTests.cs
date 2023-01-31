using FluentAssertions;
using NaptanDataPrototype.Functions;
using Xunit;

namespace NaptanDataPrototypeTests;

public class FunctionsTests
{
    [Fact]
    public void ShouldMatch()
    {
        var val1 = 10.00001;
        var val2 = 10.00001;
        var result = Functions.IsMatching(val1, val2);

        result.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(10.00002, 10.00001)]
    [InlineData(10.00001, 10.00002)]
    public void ShouldNotMatch(double val1, double val2)
    {
        var result = Functions.IsMatching(val1, val2);

        result.Should().BeFalse();
    }
    
    [Fact]
    public void ShouldMatchWhenAcceptableDifferenceBeenSent()
    {
        var val1 = 10.00002;
        var val2 = 10.00001;
        var result = Functions.IsMatching(val1, val2, 0.00001);

        result.Should().BeTrue();
    }
    
    [Fact]
    public void ShouldNotMatchWhenAcceptableDifferenceBeenSent()
    {
        var val1 = 10.00005;
        var val2 = 10.00001;
        var result = Functions.IsMatching(val1, val2, 0.00001);

        result.Should().BeFalse();
    }
}