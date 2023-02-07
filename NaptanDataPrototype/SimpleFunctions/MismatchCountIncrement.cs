namespace NaptanDataPrototype.SimpleFunctions;

public static partial class Function
{
    public static IDictionary<int, int> MismatchCountIncrement(IDictionary<int, int> misMatchCounts, int atcoCode)
    {
        if (!misMatchCounts.ContainsKey(atcoCode))
        {
            misMatchCounts.Add(atcoCode, 1);
        }
        else
        {
            misMatchCounts[atcoCode] += 1;
        }

        return misMatchCounts;
    }
}