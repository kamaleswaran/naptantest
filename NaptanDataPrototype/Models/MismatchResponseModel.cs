namespace NaptanDataPrototype.Models;

public class MismatchResponseModel
{
    public int TotalProcessed { get; set; }
    
    public IDictionary<int, int> MisMatchCountDictionary { get; set; }
    
    public IDictionary<int, int> MisMatchLatitudeCount { get; set; }
    
    public IDictionary<int, int> MisMatchLongitudeCount { get; set; }

    public List<double> MisMatchLatitude { get; set; }
    
    public List<double> MisMatchLongitude { get; set; }
}