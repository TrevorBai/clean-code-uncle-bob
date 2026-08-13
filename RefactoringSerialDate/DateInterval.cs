public static class DateIntervalHelper
{
    public enum DateInterval
    {
        Open,
        ClosedLeft,
        ClosedRight,
        Closed       
    }

    public static bool IsIn(this DateInterval interval, int d, int left, int right)
    {
        return interval switch
        {
            DateInterval.Open        => d > left && d < right,
            DateInterval.ClosedLeft  => d >= left && d < right,
            DateInterval.ClosedRight => d > left && d <= right,
            DateInterval.Closed      => d >= left && d <= right,
            _ => throw new ArgumentException($"Unknown interval {interval}")
        };     
    }
 
}


