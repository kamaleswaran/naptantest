using NaptanDataPrototype.Models;
using Serilog;

namespace NaptanDataPrototype.Services;

public class OsDataFromFile
{
    private int badRecordCount;
    
    public Task<List<NaptanModel>> GetLatitudeLongitude()
    {
        var locationData = File.ReadLines(@"./Files/OsUKData.csv");

        var naptanModels = new List<NaptanModel>();
        
        foreach (var data in locationData)
        {
            if (string.IsNullOrEmpty(data) || data.Split(',').Length < 4)
            {
                badRecordCount++;
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

        Log.Information($"Bad records in cache files are: {badRecordCount}");
        return Task.FromResult(naptanModels);
    }
}