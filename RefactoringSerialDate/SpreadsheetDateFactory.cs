public class SpreadsheetDateFactory : DayDateFactory
{
    public DayDate MakeDate(int ordinal)
    {
        return new SpreadsheetDate(ordinal);
    }

    public DayDate MakeDate(int day, DayDate.Month month, int year)
    {
        return new SpreadsheetDate(day, month, year);
    }

    public DayDate MakeDate(int day, int month, int year)
    {
        return new SpreadsheetDate(day, month, year);    
    }

    public DayDate MakeDate(DateOnly date)
    {
        var calendar = new GregorianCalendar();
        calendar.SetTime(date);
        return new SpreadsheetDate(
            calendar.Get(Calendar.DATE),
            DayDate.Month.Make(calendar.Get(Calendar.MONTH) + 1),
            calendar.Get(Calendar.YEAR)
        );  
    }

    protected int GetMinimumYear()
    {
        return SpreadsheetDate.MINIMUM_YEAR_SUPPORTED;
    }

    protected int GetMaximumYear()
    {
        return SpreadsheetDate.MAXIMUM_YEAR_SUPPORTED;
    }

}
