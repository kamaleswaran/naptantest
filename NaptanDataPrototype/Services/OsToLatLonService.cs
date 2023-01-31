using System.Xml.Linq;
using NaptanDataPrototype.Models;

namespace NaptanDataPrototype;

public class OsToLatLonService
{
    public async Task<LocationModel> GetLatitudeLongitude(int easting, int northing)
    {
        //throw new NotImplementedException();
        
        //HttpClient
        var locationModel = new LocationModel();
        var url = $"https://api.getthedata.com/bng2latlong/{easting}/{northing}";
        
        var httpClient = new HttpClient();

        var conversionRequest = await httpClient.GetStringAsync(url);
        XDocument doc = XDocument.Parse(conversionRequest);
        locationModel.Longitude = Convert.ToDouble(doc.Root.Element("longitude").Value);
        locationModel.Latitude = Convert.ToDouble(doc.Root.Element("latitude").Value);
        return locationModel;
    }
}