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

    // ...

    
}
