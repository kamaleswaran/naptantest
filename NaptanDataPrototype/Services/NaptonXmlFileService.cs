using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using NaptanDataPrototype.Models;

namespace NaptanDataPrototype.Services;

public class NaptonXmlFileService
{
    public NaptanModel GetLocation()
    {
        XmlTextReader reader = new XmlTextReader(@"./Files/Naptan-oneStopPoint.xml");
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(reader);
        
        XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
        nsmgr.AddNamespace("ns", xmlDoc.DocumentElement.NamespaceURI);

        XmlNode node = xmlDoc.SelectSingleNode("/ns:NaPTAN/ns:StopPoint/ns:Place/ns:Location/ns:Translation", nsmgr);
        
        var easting = Convert.ToInt32(node["Easting"].InnerText);
        var northing = Convert.ToInt32(node["Northing"].InnerText);
        var latitude = Convert.ToDouble(node["Latitude"].InnerText);
        var longitude = Convert.ToDouble(node["Longitude"].InnerText);
        
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
        
        
        // XmlReader reader = XmlReader.Create(@"./Files/Naptan-oneStopPoint.xml");
        // XElement root = XElement.Load(reader);
        // XmlNameTable nameTable = reader.NameTable;
        // XmlNamespaceManager namespaceManager = new XmlNamespaceManager(nameTable);
        // namespaceManager.AddNamespace("", "http://www.naptan.org.uk/");
        // IEnumerable<XElement> list1 = root.XPathSelectElements("StopPoint/Place/Location/Translation", namespaceManager);
        //
        // return null;
    }
}