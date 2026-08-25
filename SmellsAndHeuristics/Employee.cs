using System;

// This highlights how payroll system works.
public abstract class Employee
{
    public abstract int GetTenthsWorked();
    public abstract int GetTenthRate();

    /// <summary>
    /// Returns pay in cents to avoid float errors.
    /// If using dollars, it would have to calc using floats.
    /// </summary>
    public int StraightPay()
    {
        return GetTenthsWorked() * GetTenthRate();
    }

    public int OverTimePay()
    {
        int overTimeTenths = Math.Max(0, GetTenthsWorked() - 400);
        int overTimePay = OverTimeBonus(overTimeTenths);
        return StraightPay() + overTimePay;
    }

    private int OverTimeBonus(int overTimeTenths)
    {
        double bonus = 0.5 * GetTenthRate() * overTimeTenths;
        return (int)Math.Round(bonus, MidpointRounding.AwayFromZero);
    }

    
}
