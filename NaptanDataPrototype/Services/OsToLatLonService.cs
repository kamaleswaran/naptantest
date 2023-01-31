using System.Text.Json;
using NaptanDataPrototype.Models;

namespace NaptanDataPrototype.Services;

public class OsToLatLonService
{
    public async Task<LocationModel?> GetLatitudeLongitude(int easting, int northing)
    {
        var url = $"https://api.getthedata.com/bng2latlong/{easting}/{northing}";
        
        var httpClient = new HttpClient();

        var response = await httpClient.GetStringAsync(url);

        var jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        return JsonSerializer.Deserialize<LocationModel>(response, jsonSerializerOptions);
    }
}