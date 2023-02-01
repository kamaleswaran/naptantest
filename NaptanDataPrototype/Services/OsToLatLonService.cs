using System.Text.Json;
using NaptanDataPrototype.Models;

namespace NaptanDataPrototype.Services;

public class OsToLatLonService
{
    public async Task<LocationModel?> GetLatitudeLongitude(int easting, int northing)
    {
        var exceptionCount = 0;
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
            Console.WriteLine($"Exception count = {exceptionCount}");
            Console.WriteLine(exception);
            return null;
        }
    }
}