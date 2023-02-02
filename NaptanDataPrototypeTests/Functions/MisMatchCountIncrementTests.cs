using FluentAssertions;
using Xunit;

namespace NaptanDataPrototypeTests.Functions;

public class MisMatchCountIncrementTests
{
    [Fact]
    public void ShouldAddKeyToDictionary()
    {
        IDictionary<int, int> misMatchCounts = new Dictionary<int, int>();
        var atcoCode = 123;
        misMatchCounts = NaptanDataPrototype.Functions.Functions.MismatchCountIncrement(misMatchCounts, atcoCode);

        misMatchCounts.ContainsKey(atcoCode).Should().BeTrue();
        misMatchCounts[atcoCode].Should().Be(1);
    }
    
    [Fact]
    public void ShouldAddValueToExistingKeyInDictionary()
    {
        IDictionary<int, int> misMatchCounts = new Dictionary<int, int>();
        var atcoCode = 123;

        NaptanDataPrototype.Functions.Functions.MismatchCountIncrement(misMatchCounts, atcoCode);
        
        var result = NaptanDataPrototype.Functions.Functions.MismatchCountIncrement(misMatchCounts, atcoCode);

        result[atcoCode].Should().Be(2);
    }
}