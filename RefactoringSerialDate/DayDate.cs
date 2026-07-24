using System;

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

    /// <summary>
    /// The method name indicates it would return another object instead of mutating the existing object.
    /// </summary>
    public DayDate PlusDays(int days)
    {
        return DayDateFactory.MakeDate(ToOrdinal() + days);
    }

    /// <summary>
    /// The method name indicates it would return another object instead of mutating the existing object.
    /// </summary>
    public DayDate PlusMonths(int months)
    {
        int thisMonthAsOrdinal = 12 * GetYear() + (int)GetMonth() - 1;
        int resultMonthAsOrdinal = thisMonthAsOrdinal + months;
        int resultYear = resultMonthAsOrdinal / 12;
        Month resultMonth = MonthHelper.FromIndex(resultMonthAsOrdinal % 12 + 1);      
        int lastDayOfResultMonth = MonthHelper.LastDayOfMonth(resultMonth, resultYear);
        int resultDay = Math.Min(GetDayOfMonth(), lastDayOfResultMonth);
        return DayDateFactory.MakeDate(resultDay, resultMonth, resultYear);
    }

    /// <summary>
    /// The method name indicates it would return another object instead of mutating the existing object.
    /// </summary>
    public DayDate PlusYears(int years)
    {
        int resultYear = GetYear() + years;
        int lastDayOfMonthInResultYear = MonthHelper.LastDayOfMonth(GetMonth(), resultYear);
        int resultDay = Math.Min(GetDayOfMonth(), lastDayOfMonthInResultYear);
        return DayDateFactory.MakeDate(resultDay, GetMonth(), resultYear);
    }

    public DayDate GetPreviousDayOfWeek(Day targetDayOfWeek)
    {
        int offsetToTarget = (int)targetDayOfWeek - (int)GetDayOfWeek();
        if (offsetToTarget >= 0) offsetToTarget -= 7;
        return PlusDays(offsetToTarget);  
    }

    public DayDate GetFollowingDayOfWeek(Day targetDayOfWeek)
    {
        int offsetToTarget = (int)targetDayOfWeek - (int)GetDayOfWeek();
        if (offsetToTarget <= 0) offsetToTarget += 7;
        return PlusDays(offsetToTarget);
    }

    public DayDate GetNearestDayOfWeek(Day targetDay)
    {
        int offsetToThisWeeksTarget = (int)targetDay - (int)GetDayOfWeek();
        int offsetToFutureTarget = (offsetToThisWeeksTarget + 7) % 7;
        int offsetToPreviousTarget = offsetToFutureTarget - 7;

        if (offsetToFutureTarget > 3)
            return PlusDays(offsetToPreviousTarget);
        return PlusDays(offsetToFutureTarget);
    }

    public DayDate GetEndOfMonth()
    {
        Month month = GetMonth();
        int year = GetYear();
        int lastDay = MonthHelper.LastDayOfMonth(month, year);
        return DayDateFactory.MakeDate(lastDay, month, year);
    }

    public abstract int GetYear();
    public abstract Month GetMonth();
    public abstract int GetDayOfMonth();
    public abstract Day GetDayOfWeek();
    public abstract int ToOrdinal();
    
    public abstract int CompareTo(DayDate other);
    public abstract bool Equals(DayDate other);
    // + override object.Equals, GetHashCode, and operators if desired 
}
