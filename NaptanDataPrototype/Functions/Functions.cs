namespace NaptanDataPrototype.Functions;

public class Functions
{
    public static bool IsMatchingValues(double value1, double value2, double acceptableDifference = 0.00000)
    {
        var val1 = value1;
        var val2 = value2;

        if (value1 < value2)
        {
            val1 = value2;
            val2 = value1;
        }
        
        return Math.Round((val1 - val2), 5) <= Math.Round(acceptableDifference, 5);
    }
}