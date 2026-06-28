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
    
    public abstract int CompareTo(DayDate other);
    public abstract bool Equals(DayDate other);
    // + override object.Equals, GetHashCode, and operators if desired 
}
