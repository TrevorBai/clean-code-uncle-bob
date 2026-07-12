[Serializable]
public abstract class DayDate : IComparable<DayDate>, IEquatable<DayDate>
{
    public enum WeekInMonth
    {
        First = 1,
        Second = 2,
        Third = 3,
        Fourth = 4,
        Last = 0
    }

    public static bool IsLeapYear(int year) {
        bool fourth = year % 4 == 0;
        bool hundredth = year % 100 == 0;
        bool fourHundredth = year % 400 == 0;
        return fourth && (!hundredth || fourHundredth);
    }

    public DayDate AddDays(int days)
    {
        return DayDateFactory.MakeDate(ToOrdinal() + days);
    }

    public abstract int ToOrdinal();
    public abstract int CompareTo(DayDate other);
    public abstract bool Equals(DayDate other);
    // + override object.Equals, GetHashCode, and operators if desired 
}
