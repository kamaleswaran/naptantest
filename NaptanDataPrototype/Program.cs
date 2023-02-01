using System.Diagnostics;
using NaptanDataPrototype.Functions;
using NaptanDataPrototype.Services;

var naptanData = new NaptonXmlFileService();

var xmlLocations = naptanData.GetLocation(@"./Files/Brighton.xml");

var bng2latlongService = new OsToLatLonService();

var misMatchCount = 0;
var misMatchLatitudeCount = 0;
var misMatchLongitudeCount = 0;

var stopWatch = new Stopwatch();
stopWatch.Start();
foreach (var xmlLocation in xmlLocations)
{
    var locationService = await bng2latlongService.GetLatitudeLongitude(xmlLocation.Easting, xmlLocation.Northing);

    if (!Functions.IsMatching(locationService.Latitude, xmlLocation.TruncatedLatitude, 0.00005)
        || !Functions.IsMatching(locationService.Longitude, xmlLocation.TruncatedLongitude, 0.00005))
    {
        misMatchCount++;
        
        if(!Functions.IsMatching(locationService.Latitude, xmlLocation.TruncatedLatitude))
        {
            misMatchLatitudeCount++;
            Console.WriteLine("MisMatching latitude");
        }
    
        if(!Functions.IsMatching(locationService.Longitude, xmlLocation.TruncatedLongitude, 0.00000))
        {
            misMatchLongitudeCount++;
            Console.WriteLine("MisMatching longitude");
        }
    }
    else
    {
        Console.WriteLine("Matching lat/long");
    }
    
    // Console.WriteLine($"XML Latitude value = {xmlLocation.TruncatedLatitude}, Converted Latitude = {locationService.Latitude}");
    // Console.WriteLine($"XML Longitude value = {xmlLocation.TruncatedLongitude}, Converted Longitude = {locationService.Longitude}");
}
stopWatch.Stop();

Console.WriteLine($"Total count = {xmlLocations.Count}");
Console.WriteLine($"Mismatch count = {misMatchCount}");
Console.WriteLine($"Mismatch Latitude count = {misMatchLatitudeCount}");
Console.WriteLine($"Mismatch Longitude count = {misMatchLongitudeCount}");
Console.WriteLine($"Total time took to run the file = {stopWatch.Elapsed.Hours}h:{stopWatch.Elapsed.Minutes}m:{stopWatch.Elapsed.Seconds}s");