using System.Threading.Tasks;
using FluentAssertions;
using NaptanDataPrototype.Services;
using Xunit;

namespace NaptanDataPrototypeTests;

public class OsToLatLonServiceTests
{
    [Fact]
    public async Task ShouldConvertEastingNorthingToLatitudeLongitude()
    {
        var sut = new OsToLatLongService();

        var easting = 497900;
        var northing = 158836;
        var location = await sut.GetLatitudeLongitude(easting, northing);

        location?.Latitude.Should().Be(51.32021);
        location?.Longitude.Should().Be(-0.59652);
    }
}