using System.Diagnostics;
using NaptanDataPrototype.Functions;
using NaptanDataPrototype.Services;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("logger.log")
    .WriteTo.Console()
    .CreateLogger();

var naptanData = new NaptonXmlFileService();

Log.Information("Processing xml file...");
var xmlLocations = naptanData.GetLocation(@"./Files/NaPTAN.xml");

Log.Information($"Xml file location loaded! Total xmlLocations count = {xmlLocations.Count}");

var bng2latlongService = new OsToLatLonService();

IDictionary<int, int> misMatchCountDictionary = new Dictionary<int, int>();
IDictionary<int, int> misMatchLatitudeCount = new Dictionary<int, int>();
IDictionary<int, int> misMatchLongitudeCount = new Dictionary<int, int>();

var stopWatch = new Stopwatch();
stopWatch.Start();

double acceptableDifference = 0.00002;
int totalProcessed = 0;

//object lockObject = new object();

await Parallel.ForEachAsync(xmlLocations, async (xmlLocation, token) =>
{
    //lock (lockObject)
    //{
        totalProcessed++;
    //}
    //Log.Information($"Running.... {stopWatch.Elapsed.Hours}h:{stopWatch.Elapsed.Minutes}m:{stopWatch.Elapsed.Seconds}s:{stopWatch.ElapsedMilliseconds}ms");
    var locationService = await bng2latlongService.GetLatitudeLongitude(xmlLocation.Easting, xmlLocation.Northing);

    if (locationService == null)
        return;

    if (!Functions.IsMatchingValues(locationService.Latitude, xmlLocation.Latitude, acceptableDifference)
        || !Functions.IsMatchingValues(locationService.Longitude, xmlLocation.Longitude, acceptableDifference))
    {
        misMatchCountDictionary = Functions.MismatchCountIncrement(misMatchCountDictionary, xmlLocation.AtcoCode);
        
        if(!Functions.IsMatchingValues(xmlLocation.Latitude, locationService.Latitude, acceptableDifference))
        {
            misMatchLatitudeCount = Functions.MismatchCountIncrement(misMatchLatitudeCount, xmlLocation.AtcoCode);
            Log.Information($"MisMatching latitude. XML AtcoCode = {xmlLocation.AtcoCode}, XML Latitude value = {xmlLocation.Latitude}, Converted Latitude = {locationService.Latitude}");
        }
    
        if(!Functions.IsMatchingValues(xmlLocation.Longitude, locationService.Longitude, acceptableDifference))
        {
            misMatchLongitudeCount = Functions.MismatchCountIncrement(misMatchLongitudeCount, xmlLocation.AtcoCode);
            Log.Information($"MisMatching longitude. XML AtcoCode = {xmlLocation.AtcoCode}, XML Longitude value = {xmlLocation.Longitude}, Converted Longitude = {locationService.Longitude}");
        }
    }
});

stopWatch.Stop();

Log.Information($"Total count = {xmlLocations.Count}");
Log.Information($"Total processed count = {totalProcessed}");

foreach (var key in misMatchCountDictionary.Keys)
{
    Log.Information($"Key: {key}. MismatchCount = {misMatchCountDictionary[key]}");
}

foreach (var key in misMatchLatitudeCount.Keys)
{
    Log.Information($"Key: {key}. MismatchLatitudeCount = {misMatchLatitudeCount[key]}");
}

foreach (var key in misMatchLongitudeCount.Keys)
{
    Log.Information($"Key: {key}. MismatchLongitudeCount = {misMatchLongitudeCount[key]}");
}

Log.Information($"Total time took to run the file = {stopWatch.Elapsed.Hours}h:{stopWatch.Elapsed.Minutes}m:{stopWatch.Elapsed.Seconds}s");