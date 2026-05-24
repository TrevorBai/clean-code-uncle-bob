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

    [Test]
    public void TestStartSame()
    {
        string failure = new ComparisonCompactor(1, "ba", "bc").Compact(null);
        AssertEquals("expected:<b[a]> but was:<b[c]>", failure);
    }

    [Test]
    public void TestEndSame()
    {
        string failure = new ComparisonCompactor(1, "ab", "cb").Compact(null);
        AssertEquals("expected:<[a]b> but was:<[c]b>", failure);
    }

    [Test]
    public void TestSame()
    {
        string failure = new ComparisonCompactor(1, "ab", "ab").Compact(null);
        AssertEquals("expected:<ab> but was:<ab>", failure);
    }
    
    [Test]
    public void TestNoContextStartAndEndSame()
    {
        string failure = new ComparisonCompactor(0, "abc", "adc").Compact(null);
        AssertEquals("expected:<...[b]...> but was:<...[d]...>", failure);
    }

    [Test]
    public void TestStartAndEndContext()
    {
        string failure = new ComparisonCompactor(1, "abc", "adc").Compact(null);
        AssertEquals("expected:<a[b]c> but was:<a[d]c>", failure);
    }

    [Test]
    public void TestStartAndEndContextWithEllipses()
    {
        string failure = new ComparisonCompactor(1, "abcde", "abfde").Compact(null);
        AssertEquals("expected:<...b[c]d...> but was:<...b[f]d...>", failure);
    }

    [Test]
    public void TestComparisonErrorStartSameComplete()
    {
        string failure = new ComparisonCompactor(2, "ab", "abc").Compact(null);
        AssertEquals("expected:<ab[]> but was:<ab[c]>", failure);
    }    
    
    [Test]
    public void TestComparisonErrorEndSameComplete()
    {
        string failure = new ComparisonCompactor(0, "bc", "abc").Compact(null);
        AssertEquals("expected:<[]...> but was:<[a]...>", failure);
    }

    [Test]
    public void TestComparisonErrorEndSameCompleteContext()
    {
        string failure = new ComparisonCompactor(2, "bc", "abc").Compact(null);
        AssertEquals("expected:<[]bc> but was:<[a]bc>", failure);
    }

    [Test]
    public void TestComparisonErrorOverlapingMatches()
    {
        string failure = new ComparisonCompactor(0, "abc", "abbc").Compact(null);
        AssertEquals("expected:<...[]...> but was:<...[b]...>", failure);
    }

    [Test]
    public void TestComparisonErrorOverlapingMatchesContext()
    {
        string failure = new ComparisonCompactor(2, "abc", "abbc").Compact(null);
        AssertEquals("expected:<ab[]c> but was:<ab[b]c>", failure);
    }

    [Test]
    public void TestComparisonErrorOverlapingMatches2()
    {
        string failure = new ComparisonCompactor(0, "abcdde", "abcde").Compact(null);
        AssertEquals("expected:<...[d]...> but was:<...[]...>", failure);
    }

    [Test]
    public void TestComparisonErrorOverlapingMatches2Context()
    {
        string failure = new ComparisonCompactor(2, "abcdde", "abcde").Compact(null);
        AssertEquals("expected:<...cd[d]e> but was:<...cd[]e>", failure);
    }

    [Test]
    public void TestComparisonErrorWithActualNull()
    {
        string failure = new ComparisonCompactor(0, "a", null).Compact(null);
        AssertEquals("expected:<a> but was:<null>", failure);
    }

    [Test]
    public void TestComparisonErrorWithActualNullContext()
    {
        string failure = new ComparisonCompactor(2, "a", null).Compact(null);
        AssertEquals("expected:<a> but was:<null>", failure);
    }

    [Test]
    public void TestComparisonErrorWithExpectedNull()
    {
        string failure = new ComparisonCompactor(0, null, "a").Compact(null);
        AssertEquals("expected:<null> but was:<a>", failure);
    }

    [Test]
    public void TestComparisonErrorWithExpectedNullContext()
    {
        string failure = new ComparisonCompactor(2, null, "a").Compact(null);
        AssertEquals("expected:<null> but was:<a>", failure);
    }

    // This test is so interesting.
    [Test]
    public void TestBug609972()
    {
        string failure = new ComparisonCompactor(10, "S&P500", "0").Compact(null);
        AssertEquals("expected:<[S&P50]0> but was:<[]0>", failure);
    }
    
}
