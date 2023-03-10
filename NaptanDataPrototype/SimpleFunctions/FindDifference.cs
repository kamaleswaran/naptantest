namespace NaptanDataPrototype.SimpleFunctions;

public static partial class Function
{
    public static double FindDifference(double value1, double value2)
    {
        var val1 = value1;
        var val2 = value2;

        if (value1 < value2)
        {
            val1 = value2;
            val2 = value1;
        }

        val1 = Math.Round(val1, 5);
        val2 = Math.Round(val2, 5);
        
        return Math.Round(val1 - val2, 5);
    }
}