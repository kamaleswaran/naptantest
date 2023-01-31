namespace NaptanDataPrototype.Functions;

public class Functions
{
    public static bool IsMatching(double locationServiceValue, double xmlValue, double acceptedDifference = 0.00000)
    {
        double val1 = locationServiceValue;
        double val2 = xmlValue;

        if (locationServiceValue < xmlValue)
        {
            val1 = xmlValue;
            val2 = locationServiceValue;
        }
        
        return val1 - val2 <= acceptedDifference;
    } 
}