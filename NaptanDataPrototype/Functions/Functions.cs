namespace NaptanDataPrototype.Functions;

public class Functions
{
    public static bool IsMatching(double value1, double value2, double acceptableDifference = 0.00000)
    {
        // var places = 5;
        // var multiplier = Math.Pow(10, places);
        // var truncatedLocationServiceValue = Math.Truncate(locationServiceValue * multiplier) / multiplier;
        // var truncatedXmlValue = Math.Truncate(xmlValue * multiplier) / multiplier;

        // var findDecimal = value1.ToString().IndexOf('.');
        // var test = value1.ToString().Substring(0, findDecimal + 6);

        var val1 = value1;
        var val2 = value2;

        if (value1 < value2)
        {
            val1 = value2;
            val2 = value1;
        }
        
        return Math.Round((val1 - val2), 4) <= acceptableDifference;
    }

    private static double TruncateValue(double value)
    {
        var truncatedValue = value.ToString().Substring(0, 8);

        return Convert.ToDouble(truncatedValue);
    }
}