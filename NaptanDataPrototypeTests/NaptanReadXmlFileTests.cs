using NaptanDataPrototype;
using Xunit;
using FluentAssertions;
using NaptanDataPrototype.Models;

namespace NaptanDataPrototypeTests;

public class NaptanReadXmlFileTests
{
    [Fact]
    public void GetEastingNorthingFromNaptaonStopPoint()
    {
        var naptanXmlFile = new NaptonXmlFile();

        NaptanModel naptonResponse = naptanXmlFile.GetLocation();

        naptonResponse.Easting.Should().Be(497900);
        naptonResponse.Northing.Should().Be(158836);
        naptonResponse.Latitude.Should().Be(51.3202179);
        naptonResponse.Longitude.Should().Be(-0.5965128);
    }
}