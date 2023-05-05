/* The book said, I quote, "My general rule for switch statement is that they can be
 * tolerated if they appear only once, are used to create polymorphic objects, and are
 * hidden behind an inheritance relationship so that the rest of the system can't see
 * them." Basically, switch statement is usable, but the author thinks it should be used
 * to create polymorphic objects. The author also says there are exceptions.
 */

public abstract class Employee
{
    public abstract bool IsPayday();
    public abstract Money CalculatePay();
    public abstract void DeliverPay(Money pay);
}

public interface EmployeeFactory
{
    public Employee MakeEmployee(EmployeeRecord employeeRecord);
}

public class EmployeeFactoryImplementation : EmployeeFactory
{
    public Employee MakeEmployee(EmployeeRecord employeeRecord)
    {
        switch (employeeRecord.Type)
        {
            case Commissioned:
                return new CommissionedEmployee(employeeRecord);
            case Hourly:
                return new HourlyEmployee(employeeRecord);
            case Salaried:
                return new SalariedEmployee(employeeRecord);
            default: return null;
        }
    }
}
