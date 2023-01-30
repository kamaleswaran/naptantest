using FluentAssertions;
using NaptanDataPrototype;
using Xunit;

namespace NaptanDataPrototypeTests;

public class OsToLatLonServiceTests
{
    [Fact]
    public void ShouldConvertEastingNorthingToLatitudeLongitude()
    {
        var sut = new OsToLatLonService();

        var easting = 497900;
        var northing = 158836;
        var location = sut.GetLatitudeLongitude(easting, northing);

        location.Latitude.Should().Be(51.32021);
        location.Longitude.Should().Be(-0.59652);
    }
}