// Might need to tweak and add some NUnit dependencies, but I would like
// to add the gist here.

public class ComparisonCompactorTest
{
    [Test]
    public void TestMessage()
    {
        string failure = new ComparisonCompactor(0, "b", "c").Compact("a");
        AssertTrue("a expected:<[b]> but was:<[c]>".Equals(failure));
    }



    
}
