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
    public void GetEastingNorthingFromNaptaonStopPointForBrighton()
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

    [Fact]
    public void ShouldGetAptcoCode()
    {
        var naptanXmlFile = new NaptonXmlFileService();

        var naptonResponse = naptanXmlFile.GetLocation(@"./Files/Naptan-oneStopPoint.xml");

        naptonResponse[0].AtcoCode.Should().Be(400);
    }
    
    [Fact]
    public void ShouldNotGetInactiveStopPoints()
    {
        var naptanXmlFile = new NaptonXmlFileService();

        var naptonResponse = naptanXmlFile.GetLocation(@"./Files/Naptan-oneStopPoint-inactive.xml");

        naptonResponse.Count.Should().Be(0);
    }
    
    
    [Fact]
    public void ShouldGetAtcoCodeAndStopPointCounts()
    {
        var naptanXmlFile = new NaptonXmlFileService();

        var naptonResponse = naptanXmlFile.GetLocation(@"./Files/Brighton.xml");

        IDictionary<string, int> dict = new Dictionary<string, int>();

        foreach (var item in naptonResponse)
        {
            var key = item.AtcoCode.ToString();
            if (dict.ContainsKey(key))
            {
                dict[key] = dict[key] + 1;
            }
            else
            {
                dict.Add(key, 1);
            }
        }

        dict.Keys.Count.Should().BeGreaterOrEqualTo(1);
        
        // string path = "atcoCounts.csv";
        // using(StreamWriter sw = File.AppendText(path))
        // {
        //     foreach (var key in dict.Keys)
        //     {
        //         sw.WriteLine($"{key}, {dict[key]}" );
        //     }
        // }
    }
}