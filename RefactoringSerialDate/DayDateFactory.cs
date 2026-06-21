public abstract class DayDateFactory
{
    private static DayDateFactory _factory = new SpreadsheetDateFactory();

    public static void SetInstance(DayDateFactory factory)
    {
        _factory = factory;
    }

    public abstract DayDate MakeDate(int ordinal);
    public abstract DayDate MakeDate(int day, DayDate.Month month, int year);
    public abstract DayDate MakeDate(int day, int month, int year);
    public abstract DayDate MakeDate(DateOnly date);
    public abstract int GetMinimumYear();
    public abstract int GetMaximumYear();

    public static DayDate MakeDate(int ordinal)
    {
        return _factory.MakeDate(ordinal);
    }

    public static DayDate MakeDate(int day, DayDate.Month month, int year)
    {
        return _factory.MakeDate(day, month, year);
    }

    public static DayDate MakeDate(int day, int month, int year)
    {
        return _factory.MakeDate(day, month, year);
    }

    public static DayDate MakeDate(DateOnly date)
    {
        return _factory.MakeDate(date);
    }

    public static int GetMinimumYear()
    {
        return _factory.GetMinimumYear();
    }

    public static int GetMaximumYear()
    {
        return _factory.GetMaximumYear();
    }
    
}
