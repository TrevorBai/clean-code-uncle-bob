public class PortfolioTest
{
    private FixedStockExchangeStub _fixedStockExchange;
    private Portfolio _portfolio;

    protected void Setup()
    {
        _fixedStockExchange = new FixedStockExchangeStub();
        _fixedStockExchange.Fix("MSFT", 100);
        _portfolio = new Portfolio(_fixedStockExchange);
    }

    [Test]
    public void GivenFiveMSFTTotalShouldBe500()
    {
        _portfolio.Add(5, "MSFT");
        Assert.AreEqual(500, _portfolio.Value());
    }
    
}
