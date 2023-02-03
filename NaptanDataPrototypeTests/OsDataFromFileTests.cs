using FluentAssertions;
using NaptanDataPrototype.Services;
using Xunit;

namespace NaptanDataPrototypeTests;

public class OsDataFromFileTests
{
    [Fact]
    public void ShouldReadValuesFromCachedData()
    {
        var sut = new OsDataFromFile();
        var response = sut.GetLatitudeLongitude();

        response.Should().NotBeNull();
    }
}