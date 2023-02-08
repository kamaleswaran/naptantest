using System.Diagnostics;
using NaptanDataPrototype;
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

var stopWatch = new Stopwatch();
stopWatch.Start();

var mismatchedRecords = new MismatchedRecords();
var misMatchModel = await mismatchedRecords.Process(xmlLocations);

stopWatch.Stop();

Log.Information($"Total lat/long count from xml file = {xmlLocations.Count}");
Log.Information($"Total processed count = {misMatchModel.TotalProcessed}");

foreach (var key in misMatchModel.MisMatchCountDictionary.Keys)
{
    Log.Information($"AtcoCode: {key}. MismatchCount: {misMatchModel.MisMatchCountDictionary[key]}");
}

foreach (var key in misMatchModel.MisMatchLatitudeCount.Keys)
{
    Log.Information($"AtcoCode: {key}. MismatchLatitudeCount: {misMatchModel.MisMatchLatitudeCount[key]}");
}

foreach (var key in misMatchModel.MisMatchLongitudeCount.Keys)
{
    Log.Information($"AtcoCode: {key}. MismatchLongitudeCount: {misMatchModel.MisMatchLongitudeCount[key]}");
}

Log.Information($"Total time took to run the file = {stopWatch.Elapsed.Hours}h:{stopWatch.Elapsed.Minutes}m:{stopWatch.Elapsed.Seconds}s");