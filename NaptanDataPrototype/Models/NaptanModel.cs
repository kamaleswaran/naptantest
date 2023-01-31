namespace NaptanDataPrototype.Models;

public class NaptanModel
{
    public int Easting { get; set; }
    public int Northing { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    
    public double TruncatedLatitude { get; set; }
    
    public double TruncatedLongitude { get; set; }
}