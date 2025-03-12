/// <summary>
/// Try to mimic "InvocationHandler" required by the proxy API from Java proxies.
/// </summary>
public class BankProxyHandler : MarshalByRefObject
{
    private readonly IBank _bank;

    public BankProxyHandler(IBank bank)
    {
        _bank = bank;
    }

    // Method defined in InvocationHandler in Java (or here in MarshalByRefObject)
    public object Invoke(object proxy, Method method, object[] args) 
    {
        string methodName = method.GetName();
        if (methodName.Equals("GetAccounts"))
        {
            _bank.SetAccounts(GetAccountsFromDatabase());
            return _bank.GetAccounts();
        } else if (methodName.Equals("SetAccounts"))
        {
            _bank.SetAccounts((IList<Account>)args[0]);
            SetAccountsToDatabase(_bank.GetAccounts());
            return null;
        } else 
        {
            ...
        }
    }

    // Lots of details here:
    protected IList<Account> GetAccountsFromDatabase() { ... }
    protected void SetAccountsToDatabase(IList<Account> accounts) { ... }
}
