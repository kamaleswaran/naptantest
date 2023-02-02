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
            var url = $"https://api.getthedata.com/bng2latlong/{easting}/{northing}";

            using var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(url);

            var jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            return JsonSerializer.Deserialize<LocationModel>(response, jsonSerializerOptions);
        }
        catch (Exception exception)
        {
            exceptionCount++;
            Log.Information($"Exception count = {exceptionCount}");
            return null;
        }
    }
}