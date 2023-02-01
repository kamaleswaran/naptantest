using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using NaptanDataPrototype.Models;

namespace NaptanDataPrototype.Services;

public class NaptonXmlFileService
{
    public List<NaptanModel> GetLocation(string filepath)
    {
        XmlTextReader reader = new XmlTextReader(filepath);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(reader);
        
        XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
        nsmgr.AddNamespace("ns", xmlDoc.DocumentElement.NamespaceURI);

        XmlNodeList stopPoints = xmlDoc.SelectNodes("/ns:NaPTAN/ns:StopPoints/ns:StopPoint", nsmgr);

        var places = 5;
        var multiplier = Math.Pow(10, places);
        var naptanModels = new List<NaptanModel>();
        
        foreach (XmlElement stopPoint in stopPoints)
        {
            var locationNode = stopPoint.GetElementsByTagName("Translation")[0];
            var easting = Convert.ToInt32(locationNode["Easting"].InnerText);
            var northing = Convert.ToInt32(locationNode["Northing"].InnerText);
            var latitude = Convert.ToDouble(locationNode["Latitude"].InnerText);
            var longitude = Convert.ToDouble(locationNode["Longitude"].InnerText);
            
            var naptanModel = new NaptanModel
            {
                Easting = easting,
                Northing = northing,
                Latitude = latitude,
                Longitude = longitude,
                TruncatedLatitude = Math.Truncate(latitude * multiplier) / multiplier,
                TruncatedLongitude = Math.Truncate(longitude * multiplier) / multiplier
            };
            
            naptanModels.Add(naptanModel);
        }
        
        return naptanModels;
    }
}