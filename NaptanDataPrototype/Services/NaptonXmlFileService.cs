using System.Xml;
using NaptanDataPrototype.Models;

namespace NaptanDataPrototype.Services;

public class NaptonXmlFileService
{
    public NaptanModel GetLocation()
    {
        var doc = new XmlDocument();
        doc.Load(@"./Files/Naptan-oneStopPoint.xml");
        
        XmlNode node = doc.SelectSingleNode("NaPTAN/StopPoint/Place/Location/Translation");

        var easting = Convert.ToInt32(node.SelectSingleNode("Easting").InnerText);
        var northing = Convert.ToInt32(node.SelectSingleNode("Northing").InnerText);
        var latitude = Convert.ToDouble(node.SelectSingleNode("Latitude").InnerText);
        var longitude = Convert.ToDouble(node.SelectSingleNode("Longitude").InnerText);
        
        var places = 5;
        var multiplier = Math.Pow(10, places);
        
        return new NaptanModel
        {
            Easting = easting,
            Northing = northing,
            Latitude = latitude,
            Longitude = longitude,
            TruncatedLatitude = Math.Truncate(latitude * multiplier) / multiplier,
            TruncatedLongitude = Math.Truncate(longitude * multiplier) / multiplier
        };
    }
}