public Portfolio
{
    private readonly IStockExchange _stockExchange;

    public Portfolio(IStockExchange stockExchange)
    {
        _stockExchange = stockExchange;
    }

    // ...
}
