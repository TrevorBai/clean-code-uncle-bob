using System.Collections.Generic;

/// <summary>
/// The "Plain Old C# Object" (POCO, mimic to java POJO) implementing the abstraction.
/// </summary>
public class Bank : IBank
{
    private List<Account> _accounts;

    public IList<Account> GetAccounts() { return _accounts; }

    public void SetAccounts(IList<Account> accounts)
    {
        _accounts = new List<Account>();
        foreach (var account in _accounts) _accounts.Add(account);
    }
    
}
