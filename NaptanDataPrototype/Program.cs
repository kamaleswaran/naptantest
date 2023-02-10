using System.Diagnostics;
using NaptanDataPrototype;
using NaptanDataPrototype.Models;
using NaptanDataPrototype.Services;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("logger.log")
    .WriteTo.Console()
    .CreateLogger();

var naptanData = new NaptonXmlFileService();

Log.Information("Processing xml file...");
var naptanModels = naptanData.GetLocation(@"./Files/581.xml");

Log.Information($"Xml file location loaded! Total xmlLocations count = {naptanModels.Count}");

var stopWatch = new Stopwatch();
stopWatch.Start();

var mismatchedRecords = new MismatchedRecords();
var misMatchModel = await mismatchedRecords.Process(naptanModels);

stopWatch.Stop();

Log.Information($"Total lat/long count from xml file = {naptanModels.Count}");
Log.Information($"Total processed count = {misMatchModel.TotalProcessed}");

foreach (var key in misMatchModel.MisMatchCountDictionary.Keys)
{
    //Log.Information($"AtcoCode: {key}, MismatchCount: {misMatchModel.MisMatchCountDictionary[key]}");

    var totalCountsByAtco = GetTotalCountsByAtcoCode(naptanModels);
    
    Log.Information($"{key}, {misMatchModel.MisMatchCountDictionary[key]}, {totalCountsByAtco[key.ToString()]}");
}

// foreach (var key in misMatchModel.MisMatchLatitudeCount.Keys)
// {
//     Log.Information($"AtcoCode: {key}, MismatchLatitudeCount: {misMatchModel.MisMatchLatitudeCount[key]}");
// }
//
// foreach (var key in misMatchModel.MisMatchLongitudeCount.Keys)
// {
//     Log.Information($"AtcoCode: {key}, MismatchLongitudeCount: {misMatchModel.MisMatchLongitudeCount[key]}");
// }

foreach (var latitude in misMatchModel.MisMatchLatitude)
{
    Log.Information($"MismatchLatitudeDifference: {((decimal) latitude).ToString().Substring(0, 7)}");
}

foreach (var longitude in misMatchModel.MisMatchLongitude)
{
    Log.Information($"MismatchLongitudeDifference: {((decimal) longitude).ToString().Substring(0, 7)}");
}

Log.Information($"Total Latitude difference counts are {misMatchModel.MisMatchLatitude.Count}");
Log.Information($"Total Longitude difference counts are {misMatchModel.MisMatchLongitude.Count}");

Log.Information($"Total time took to run the file = {stopWatch.Elapsed.Hours}h:{stopWatch.Elapsed.Minutes}m:{stopWatch.Elapsed.Seconds}s");

IDictionary<string, int> GetTotalCountsByAtcoCode(List<NaptanModel> naptanModels)
{
    IDictionary<string, int> dict = new Dictionary<string, int>();

    foreach (var item in naptanModels)
    {
        var key = item.AtcoCode.ToString();
        if (dict.ContainsKey(key))
        {
            dict[key] = dict[key] + 1;
        }
        else
        {
            dict.Add(key, 1);
        }
    }

    return dict;
}