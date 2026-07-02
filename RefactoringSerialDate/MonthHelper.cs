using System;
using System.Globalization;

public static class MonthHelper
{
    public static enum Month
    {
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12
    }

    public static Month FromIndex(int monthIndex)
    {
        if (monthIndex < 1 || monthIndex > 12)
            throw new ArgumentException($"Invalid month index {monthIndex}");
        return (Month)monthIndex;
    }

    public static string[] GetMonthNames()
    {
        return CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
    }

    public static int Quarter(Month month)
    {
        return 1 + ((int)month - 1) / 3;
    }
    
}
