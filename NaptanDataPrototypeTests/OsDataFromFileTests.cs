using FluentAssertions;
using NaptanDataPrototype.Services;
using Xunit;

namespace NaptanDataPrototypeTests;

public class OsDataFromFileTests
{
    [Theory]
    [InlineData(529711, 107638, 50.85373, -0.15861)]
    [InlineData(529913, 105533, 50.83476, -0.15649)]
    public async void ShouldGetLatitudeLongitudeFromCachedDataFile(int easting, int northing, double latitude, double longitude)
    {
        var sut = new OsDataFromFile();
        var response = await sut.GetLatitudeLongitude(easting, northing);

        response.Latitude.Should().Be(latitude);
        response.Longitude.Should().Be(longitude);
    }
    
    [Fact]
    public async void ShouldGetNullWhenEastingNorthingNotFound()
    {
        var sut = new OsDataFromFile();

        int easting = 123;
        int northing = 456;
        
        var response = await sut.GetLatitudeLongitude(easting, northing);

        response.Should().BeNull();
    }
}