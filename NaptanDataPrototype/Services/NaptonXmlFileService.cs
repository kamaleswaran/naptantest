using System.Xml;
using NaptanDataPrototype.Models;

namespace NaptanDataPrototype;

public class NaptonXmlFileService
{
    public NaptanModel GetLocation()
    {
        var doc = new XmlDocument();
        doc.Load(@"./Files/Naptan-oneStopPoint.xml");
        
        XmlNode node = doc.SelectSingleNode("NaPTAN/StopPoint/Place/Location/Translation");

        var easting = Convert.ToDouble(node.SelectSingleNode("Easting").InnerText);
        var northing = Convert.ToDouble(node.SelectSingleNode("Northing").InnerText);
        var latitude = Convert.ToDouble(node.SelectSingleNode("Latitude").InnerText);
        var longitude = Convert.ToDouble(node.SelectSingleNode("Longitude").InnerText);
        
        return new NaptanModel
        {
            Easting = easting,
            Northing = northing,
            Latitude = latitude,
            Longitude = longitude
        };
    }
}