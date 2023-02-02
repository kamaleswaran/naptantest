using System.Diagnostics;
using NaptanDataPrototype.Functions;
using NaptanDataPrototype.Services;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("logger.log")
    .CreateLogger();

var naptanData = new NaptonXmlFileService();

Log.Information("Processing xml file locations...");
var xmlLocations = naptanData.GetLocation(@"./Files/Brighton.xml");

Log.Information("Loaded xml file locations");

var bng2latlongService = new OsToLatLonService();

IDictionary<int, int> misMatchCountDictionary = new Dictionary<int, int>();
var misMatchLatitudeCount = 0;
var misMatchLongitudeCount = 0;

var stopWatch = new Stopwatch();
stopWatch.Start();

//var parallelOptions = new ParallelOptions {MaxDegreeOfParallelism = 25 };
double acceptableDifference = 0.00001;

Log.Information($"Total xmlLocations count = {xmlLocations.Count}");

await Parallel.ForEachAsync(xmlLocations, async (xmlLocation, token) =>
{
    Log.Information($"Running.... {stopWatch.Elapsed.Hours}h:{stopWatch.Elapsed.Minutes}m:{stopWatch.Elapsed.Seconds}s:{stopWatch.ElapsedMilliseconds}ms");
    var locationService = await bng2latlongService.GetLatitudeLongitude(xmlLocation.Easting, xmlLocation.Northing);

    if (locationService == null)
        return;

    if (!Functions.IsMatchingValues(locationService.Latitude, xmlLocation.Latitude, acceptableDifference)
        || !Functions.IsMatchingValues(locationService.Longitude, xmlLocation.Longitude, acceptableDifference))
    {
        misMatchCountDictionary = Functions.MismatchCountIncrement(misMatchCountDictionary, xmlLocation.AtcoCode);
        
        if(!Functions.IsMatchingValues(locationService.Latitude, xmlLocation.Latitude, acceptableDifference))
        {
            misMatchLatitudeCount++;
            Log.Information("MisMatching latitude");
            Log.Information($"XML Latitude value = {xmlLocation.Latitude}, Converted Latitude = {locationService.Latitude}");
        }
    
        if(!Functions.IsMatchingValues(locationService.Longitude, xmlLocation.Latitude, acceptableDifference))
        {
            misMatchLongitudeCount++;
            Log.Information("MisMatching longitude");
            Log.Information($"XML Longitude value = {xmlLocation.Longitude}, Converted Longitude = {locationService.Longitude}");
        }
    }
});

stopWatch.Stop();

Log.Information($"Total count = {xmlLocations.Count}");

foreach (var key in misMatchCountDictionary.Keys)
{
    Log.Information($"Key: {key}. MismatchCount = {misMatchCountDictionary[key]}");
}

Log.Information($"Mismatch Latitude count = {misMatchLatitudeCount}");
Log.Information($"Mismatch Longitude count = {misMatchLongitudeCount}");
Log.Information($"Total time took to run the file = {stopWatch.Elapsed.Hours}h:{stopWatch.Elapsed.Minutes}m:{stopWatch.Elapsed.Seconds}s");