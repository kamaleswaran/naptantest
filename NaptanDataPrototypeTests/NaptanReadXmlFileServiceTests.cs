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

        var naptonResponse = naptanXmlFile.GetLocation(@"./Files/Naptan-oneStopPoint.xml");

        naptonResponse[0].Easting.Should().Be(497900);
        naptonResponse[0].Northing.Should().Be(158836);
        naptonResponse[0].Latitude.Should().Be(51.3202179);
        naptonResponse[0].Longitude.Should().Be(-0.5965128);
    }
    
    [Fact]
    public void GetEastingNorthingFromNaptaonStopPoint2()
    {
        var naptanXmlFile = new NaptonXmlFileService();

        var results = naptanXmlFile.GetLocation(@"./Files/Brighton.xml");

        results.Count.Should().BeGreaterThan(1);

        foreach (var item in results)
        {
            item.Easting.Should().BeGreaterThan(0);
            item.Northing.Should().BeGreaterThan(0);    
        }
    }
}