/// <summary>
/// The bad thing about this method is in each case of this
/// switch statements, i.e. CalculateCommissionedPay(..),
/// CalculateHourlyPay(..) or CalculateSalariedPay(...), there
/// are duplicate methods in them. We could refactor them using
/// polymorphism/abstract factory, and encapsulate all the details,
/// so that people won't need see them unless they really need to.
/// </summary>
public Money CalculatePay(Employee employee)
{
    switch (employee.Type)
    {
        case Commissioned:
            return CalculateCommissionedPay(employee);
        case Hourly:
            return CalculateHourlyPay(employee);
        case Salaried:
            return CalculateSalariedPay(employee);
        default: return null;
    }
}
