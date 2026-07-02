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

    public static string ToFullString(Month month)
    {
        return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName((int)month);
    }

    public static string ToShortString(Month month)
    {
        return CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName((int)month);
    }

    public static Month Parse(string s)
    {
        s = s.Trim();
    
        foreach (Month month in Enum.GetValues<Month>())
            if (Matches(month, s)) return month;
    
        if (int.TryParse(s, out int index))
            return FromIndex(index);
        throw new ArgumentException($"Invalid month {s}");
    }

    private static bool Matches(Month month, string s)
    {
        return string.Equals(s, month.ToFullString(), StringComparison.OrdinalIgnoreCase)
            || string.Equals(s, month.ToShortString(), StringComparison.OrdinalIgnoreCase);
    }
    
}
