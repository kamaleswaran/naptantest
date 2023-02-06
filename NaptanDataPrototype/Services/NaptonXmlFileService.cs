using System.Diagnostics;
using System.Xml;
using NaptanDataPrototype.Models;
using Serilog;

namespace NaptanDataPrototype.Services;

public class NaptonXmlFileService
{
    public List<NaptanModel> GetLocation(string filepath)
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();
        
        XmlTextReader reader = new XmlTextReader(filepath);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(reader);
        
        Log.Information("Xml file loaded...");
        
        XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
        nsmgr.AddNamespace("ns", xmlDoc.DocumentElement.NamespaceURI);

        XmlNodeList stopPoints = xmlDoc.SelectNodes("/ns:NaPTAN/ns:StopPoints/ns:StopPoint", nsmgr);

        var naptanModels = new List<NaptanModel>();
        
        Log.Information($"Stoppoints count = {stopPoints.Count}");

        foreach (XmlNode stopPoint in stopPoints)
        {
            if (stopPoint.Attributes["Status"].Value.ToLower() != "active")
            {
                continue;
            }
            
            var locationNode = stopPoint.SelectSingleNode("ns:Place/ns:Location/ns:Translation", nsmgr);
            if (locationNode == null)
            {
                continue;
            }
            
            var easting = Convert.ToInt32(locationNode["Easting"].InnerText);
            var northing = Convert.ToInt32(locationNode["Northing"].InnerText);
            var latitude = Convert.ToDouble(locationNode["Latitude"].InnerText);
            var longitude = Convert.ToDouble(locationNode["Longitude"].InnerText);
            var atcoCode = Convert.ToInt32(stopPoint["AtcoCode"].InnerText.Substring(0, 3));

            var naptanModel = new NaptanModel
            {
                AtcoCode = atcoCode,
                Easting = easting,
                Northing = northing,
                Latitude = latitude,
                Longitude = longitude
            };

            naptanModels.Add(naptanModel);
        }
        
        stopWatch.Stop();
        Log.Information($"{stopWatch.Elapsed.Hours}h:{stopWatch.Elapsed.Minutes}m:{stopWatch.Elapsed.Seconds}s");
        
        return naptanModels;
    }
}