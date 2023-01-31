using NaptanDataPrototype.Functions;
using NaptanDataPrototype.Services;

var naptanData = new NaptonXmlFileService();

var xmlLocation = naptanData.GetLocation();

var bng2latlongService = new OsToLatLonService();

var locationService = await bng2latlongService.GetLatitudeLongitude(xmlLocation.Easting, xmlLocation.Northing);

if(Functions.IsMatching(locationService.Latitude, xmlLocation.TruncatedLatitude)
   && Functions.IsMatching(locationService.Longitude, xmlLocation.TruncatedLongitude, 0.00000)
   )
{
    Console.WriteLine("Matching lat/long");
}
else
{
    Console.WriteLine("Mismatching lat/long");
}
// Console.WriteLine($"XML Latitude value = {xmlLocation.TruncatedLatitude}, Converted Latitude = {locationService.Latitude}");
// Console.WriteLine($"XML Longitude value = {xmlLocation.TruncatedLongitude}, Converted Longitude = {locationService.Longitude}");
