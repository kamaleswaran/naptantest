using NaptanDataPrototype.Models;
using NaptanDataPrototype.Services;
using NaptanDataPrototype.SimpleFunctions;
using Serilog;

namespace NaptanDataPrototype;

public class MismatchedRecords
{
    double acceptableDifference = 0.00002;
    int totalProcessed;
    
    IDictionary<int, int> _misMatchCountDictionary = new Dictionary<int, int>();
    IDictionary<int, int> _misMatchLatitudeCount = new Dictionary<int, int>();
    IDictionary<int, int> _misMatchLongitudeCount = new Dictionary<int, int>();

    private List<double> _misMatchLatitude = new List<double>();
    private List<double> _misMatchLongitude = new List<double>();

    public async Task<MismatchResponseModel> Process(List<NaptanModel> naptanModels)
    {
        var bng2latlongService = new OsToLatLongService();
        
        foreach (var xmlLocation in naptanModels)
        {
            totalProcessed++;
            Console.WriteLine(totalProcessed);
            var locationService = await bng2latlongService.GetLatitudeLongitude(xmlLocation.Easting, xmlLocation.Northing);

            if (locationService == null)
                continue;

            if (!Function.IsMatchingValues(locationService.Latitude, xmlLocation.Latitude, acceptableDifference)
                || !Function.IsMatchingValues(locationService.Longitude, xmlLocation.Longitude, acceptableDifference))
            {
                _misMatchCountDictionary = Function.MismatchCountIncrement(_misMatchCountDictionary, xmlLocation.AtcoCode);

                if (!Function.IsMatchingValues(xmlLocation.Latitude, locationService.Latitude, acceptableDifference))
                {
                    // _misMatchLatitudeCount = Function.MismatchCountIncrement(_misMatchLatitudeCount, xmlLocation.AtcoCode);
                    // Log.Information(
                    //     $"MisMatching latitude. XML AtcoCode = {xmlLocation.AtcoCode}, XML Latitude value = {xmlLocation.Latitude}, Latitude = {locationService.Latitude}");

                    var difference = Function.FindDifference(xmlLocation.Latitude, locationService.Latitude);
                    _misMatchLatitude.Add(difference);
                }
                
                if (!Function.IsMatchingValues(xmlLocation.Longitude, locationService.Longitude, acceptableDifference))
                {
                    // _misMatchLongitudeCount = Function.MismatchCountIncrement(_misMatchLongitudeCount, xmlLocation.AtcoCode);
                    // Log.Information(
                    //     $"MisMatching longitude. XML AtcoCode = {xmlLocation.AtcoCode}, XML Longitude value = {xmlLocation.Longitude}, Longitude = {locationService.Longitude}");

                    var difference = Function.FindDifference(xmlLocation.Longitude, locationService.Longitude);
                    _misMatchLongitude.Add(difference);
                }
            }
        }

        return new MismatchResponseModel
        {
            MisMatchCountDictionary = _misMatchCountDictionary,
            MisMatchLatitudeCount = _misMatchLatitudeCount,
            MisMatchLongitudeCount = _misMatchLongitudeCount,
            TotalProcessed = totalProcessed,
            MisMatchLatitude = _misMatchLatitude,
            MisMatchLongitude = _misMatchLongitude
        };
    }
}