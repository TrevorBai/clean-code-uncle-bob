using System.Collections.Generic;

/// <summary>
/// The abstraction of a bank.
/// </summary>
public interface IBank
{
    IList<Account> GetAccounts();
    void SetAccounts(IList<Account> accounts);
}
