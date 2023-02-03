using NaptanDataPrototype.Models;

namespace NaptanDataPrototype.Services;

public class OsDataFromFile
{
    public async Task<NaptanModel> GetLatitudeLongitude(int easting, int northing)
    {
        var locationData = File.ReadLines(@"./Files/OsUKData.csv");

        var naptanModels = new List<NaptanModel>();
        
        foreach (var data in locationData)
        {
            if (string.IsNullOrEmpty(data))
            {
                continue;
            }
            var naptanModel = new NaptanModel
            {
                Easting = Convert.ToInt32(data.Split(',')[0]),
                Northing = Convert.ToInt32(data.Split(',')[1]),
                Latitude = Convert.ToDouble(data.Split(',')[2]),
                Longitude = Convert.ToDouble(data.Split(',')[3])
            };
            
            naptanModels.Add(naptanModel);
        }

        var result = naptanModels.FirstOrDefault(n => n.Easting == easting && n.Northing == northing);

        if (result == null)
        {
            return null;
        }
        
        return new NaptanModel
        {
            Latitude = result.Latitude,
            Longitude = result.Longitude
        };
    }
}