// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using NaptanDataPrototype.Services;

Console.WriteLine("Hello, World!");

var naptanData = new NaptonXmlFileService();

var xmlLocation = naptanData.GetLocation();

Console.WriteLine(JsonSerializer.Serialize(xmlLocation));

var bng2latlongService = new OsToLatLonService();

var locationService = await bng2latlongService.GetLatitudeLongitude(xmlLocation.Easting, xmlLocation.Northing);

var places = 5;
var multiplier = Math.Pow(10, places);
var latitude = Math.Truncate(xmlLocation.Latitude * multiplier) / multiplier;
var longitude = Math.Truncate(xmlLocation.Longitude * multiplier) / multiplier;

if (locationService.Latitude - latitude <= 0.00000
    && locationService.Longitude - longitude <= 0.00005)
{
    Console.WriteLine("Matching lat/long");
}
else
{
    Console.WriteLine("Mismatching lat/long");
}
Console.WriteLine($"XML Original Latitude value = {xmlLocation.Latitude}, XML Converted Latitude = {latitude}, Converted Latitude = {locationService.Latitude}");
Console.WriteLine($"XML Original Longitude value = {xmlLocation.Longitude}, XML Converted Longitude = {longitude}, Converted Longitude = {locationService.Longitude}");