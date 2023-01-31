using Xunit;
using FluentAssertions;
using NaptanDataPrototype.Models;
using NaptanDataPrototype.Services;

namespace NaptanDataPrototypeTests;

public class NaptanReadXmlFileServiceTests
{
    [Fact]
    public void GetEastingNorthingFromNaptaonStopPoint()
    {
        var naptanXmlFile = new NaptonXmlFileService();

        NaptanModel naptonResponse = naptanXmlFile.GetLocation();

        naptonResponse.Easting.Should().Be(497900);
        naptonResponse.Northing.Should().Be(158836);
        naptonResponse.Latitude.Should().Be(51.3202179);
        naptonResponse.Longitude.Should().Be(-0.5965128);
    }
}