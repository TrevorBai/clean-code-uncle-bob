// An index program/entry point
IBank bank = (IBank) Proxy.NewProxyInstance
    (
        IBank.Class.GetClassLoader(),
        new Class[] { IBank.Class },
        new BankProxyHandler(new Bank())
    );
