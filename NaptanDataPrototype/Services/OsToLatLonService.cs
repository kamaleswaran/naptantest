using System.Text.Json;
using NaptanDataPrototype.Models;
using Serilog;

namespace NaptanDataPrototype.Services;

public class OsToLatLonService
{
    int exceptionCount;
   
    public async Task<LocationModel?> GetLatitudeLongitude(int easting, int northing)
    {
        try
        {
            var resultFromCachedData = await GetLatitudeLongitudeFromCachedData(easting, northing);

            if (resultFromCachedData != null)
            {
                return resultFromCachedData;
            }
            
            var url = $"https://api.getthedata.com/bng2latlong/{easting}/{northing}";

            using var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(url);

            var jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            
            var locationModel = JsonSerializer.Deserialize<LocationModel>(response, jsonSerializerOptions);
            
            string path = "OsUKData.csv";               
            using(StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine($"{easting},{northing},{locationModel.Latitude},{locationModel.Longitude}" );
            }

            return locationModel;
        }
        catch (Exception exception)
        {
            exceptionCount++;
            Log.Information($"Exception count = {exceptionCount}");
            return null;
        }
    }

    private async Task<LocationModel> GetLatitudeLongitudeFromCachedData(int easting, int northing)
    {
        var osData = new OsDataFromFile();
        var result = await osData.GetLatitudeLongitude(easting, northing);

        if (result == null)
        {
            return null;
        }
        
        return new LocationModel
        {
            Latitude = result.Latitude,
            Longitude = result.Longitude
        };
    }
}