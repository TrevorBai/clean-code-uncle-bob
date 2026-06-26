public static class DayHelper
{

    public enum Day
    {
        Monday = 2,
        Tuesday = 3,
        Wednesday = 4,
        Thursday = 5,
        Friday = 6,
        Saturday = 7,
        Sunday = 1
    }

    public static Day FromIndex(int index)
    {
        foreach (var day in Enum.GetValues<Day>())
        {
            if ((int)day == index) return day;
        }
        throw new ArgumentException($"Illegal day index: {index}.");
    }

    public static Day Parse(string s)
    {
        s = s.Trim();
        var culture = CultureInfo.CurrentCulture.DateTimeFormat;

        foreach (var day in Enum.GetValues<Day>())
        {
            // C# DayOfWeek is 0-based (Sunday=0); convert from our 1-based Calendar-style index
            var dayOfWeek = (DayOfWeek)((int)day % 7);

            var fullName = culture.GetDayName(dayOfWeek);
            var shortName = culture.GetAbbreviatedDayName(dayOfWeek);

            if (string.Equals(s, fullName, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(s, shortName, StringComparison.OrdinalIgnoreCase))
                return day;       
        }
        throw new ArgumentException($"{s} is not a valid weekday string");
    }

    public static string ToDisplayString(Day day)
    {
        var dayOfWeek = (DayOfWeek)((int)day % 7);
        return CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(dayOfWeek);
    }
    
}
