using System.Diagnostics;
using NaptanDataPrototype.Functions;
using NaptanDataPrototype.Services;

var naptanData = new NaptonXmlFileService();

Console.WriteLine("Processing xml file locations...");
var xmlLocations = naptanData.GetLocation(@"./Files/NaPTAN.xml");

Console.WriteLine("Loaded xml file locations");

var bng2latlongService = new OsToLatLonService();

var misMatchCount = 0;
var misMatchLatitudeCount = 0;
var misMatchLongitudeCount = 0;

var stopWatch = new Stopwatch();
stopWatch.Start();

//var parallelOptions = new ParallelOptions {MaxDegreeOfParallelism = 25 };
double acceptableDifference = 0.00001;

Console.WriteLine($"Total xmlLocations count = {xmlLocations.Count}");

await Parallel.ForEachAsync(xmlLocations, async (xmlLocation, token) =>
{
    Console.WriteLine($"Running.... {stopWatch.Elapsed.Hours}h:{stopWatch.Elapsed.Minutes}m:{stopWatch.Elapsed.Seconds}s:{stopWatch.ElapsedMilliseconds}ms");
    var locationService = await bng2latlongService.GetLatitudeLongitude(xmlLocation.Easting, xmlLocation.Northing);

    if (locationService == null)
        return;

    if (!Functions.IsMatchingValues(locationService.Latitude, xmlLocation.Latitude, acceptableDifference)
        || !Functions.IsMatchingValues(locationService.Longitude, xmlLocation.Longitude, acceptableDifference))
    {
        misMatchCount++;
        
        if(!Functions.IsMatchingValues(locationService.Latitude, xmlLocation.Latitude, acceptableDifference))
        {
            misMatchLatitudeCount++;
            Console.WriteLine("MisMatching latitude");
            Console.WriteLine($"XML Latitude value = {xmlLocation.Latitude}, Converted Latitude = {locationService.Latitude}");
        }
    
        if(!Functions.IsMatchingValues(locationService.Longitude, xmlLocation.Latitude, acceptableDifference))
        {
            misMatchLongitudeCount++;
            Console.WriteLine("MisMatching longitude");
            Console.WriteLine($"XML Longitude value = {xmlLocation.Longitude}, Converted Longitude = {locationService.Longitude}");
        }
    }
});

stopWatch.Stop();

Console.WriteLine($"Total count = {xmlLocations.Count}");
Console.WriteLine($"Mismatch count = {misMatchCount}");
Console.WriteLine($"Mismatch Latitude count = {misMatchLatitudeCount}");
Console.WriteLine($"Mismatch Longitude count = {misMatchLongitudeCount}");
Console.WriteLine($"Total time took to run the file = {stopWatch.Elapsed.Hours}h:{stopWatch.Elapsed.Minutes}m:{stopWatch.Elapsed.Seconds}s");