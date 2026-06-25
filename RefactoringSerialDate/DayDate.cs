[Serializable]
public abstract class DayDate : IComparable<DayDate>, IEquatable<DayDate>
{
    public static enum Month : byte
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

    public enum WeekInMonth
    {
        First = 1,
        Second = 2,
        Third = 3,
        Fourth = 4,
        Last = 0
    }
    
    public static class MonthHelper
    {
        public static Month FromIndex(int monthIndex)
        {
            if (monthIndex < 1 || monthIndex > 12)
                throw new ArgumentException($"Invalid month index {monthIndex}");
            return (Month)monthIndex;
        }       
    }

    
    public abstract int CompareTo(DayDate other);
    public abstract bool Equals(DayDate other);
    // + override object.Equals, GetHashCode, and operators if desired 
}
