using FluentAssertions;
using NaptanDataPrototype.Services;
using Xunit;

namespace NaptanDataPrototypeTests;

public class OsDataFromFileTests
{
    [Fact]
    public async Task ShouldReadValuesFromCachedData()
    {
        var sut = new OsDataFromFile();
        var response = await sut.GetLatitudeLongitude();

        response.Should().NotBeNull();
    }
}