public class ComparisonCompactor
{
    private const string ELLIPSIS = "...";
    private const string DELTA_END = "]";
    private const string DELTA_START = "[";

    private int _contextLength;
    private string _expected;
    private string _actual;
    private int _prefixLength;
    private int _suffixLength;

    public ComparisonCompactor(int contextLength, string expected, string actual)
    {
        _contextLength = contextLength;
        _expected = expected;
        _actual = actual;
    }

    public string FormatCompactedComparison(string message)
    {
        string compactExpected = _expected;
        string compactActual = _actual;
        if (ShouldBeCompacted())
        {
            FindCommonPrefixAndSuffix();       
            compactExpected = Compact(_expected);
            compactActual = Compact(_actual);
        }
        return Assert.Format(message, compactExpected, compactActual);
    }

    private bool ShouldBeCompacted()
    {
        return !ShouldNotBeCompacted();
    }

    private bool ShouldNotBeCompacted()
    {
        return _expected == null || _actual == null || _expected.Equals(_actual);
    }

    private void FindCommonPrefixAndSuffix()
    {
        FindCommonPrefix();
        // Claude says there is a subtle bug at the for loop when the suffix and prefix regions exactly meet.
        // Not verify it yet. It's an off-by-one bug from Claude.
        for (_suffixLength = 0; !SuffixOverlapsPrefix(); _suffixLength++)
        {
            if (CharFromEnd(_expected, _suffixLength) != CharFromEnd(_actual, _suffixLength)) break;
        }
    }

    private char CharFromEnd(string s, int i)
    {
        return s[s.Length - i - 1];
    }

    private bool SuffixOverlapsPrefix()
    {
        return _actual.Length - _suffixLength <= _prefixLength ||
            _expected.Length - _suffixLength <= _prefixLength;
    }

    private void FindCommonPrefix()
    {
        _prefixLength = 0;
        int end = Math.Min(_expected.Length, _actual.Length);
        for (; _prefixLength < end; _prefixLength++)
        {
            if (_expected[_prefixLength] != _actual[_prefixLength]) break;
        }
    }

    private string Compact(string s)
    {
        return new StringBuilder()
            .Append(StartingEllipsis())
            .Append(StartingContext())
            .Append(DELTA_START)
            .Append(Delta(s))
            .Append(DELTA_END)
            .Append(EndingContext())
            .Append(EndingEllipsis())
            .ToString();
    }

    private string StartingEllipsis()
    {
        return _prefixLength > _contextLength ? ELLIPSIS : "";
    }

    private string StartingContext()
    {
        int contextStart = Math.Max(0, _prefixLength - _contextLength);
        int contextEnd = _prefixLength;
        return _expected.Substring(contextStart, contextEnd - contextStart);
    }

    private string Delta(string s)
    {
        int deltaStart = _prefixLength;
        int deltaEnd = s.Length - _suffixLength;
        return s.Substring(deltaStart, deltaEnd - deltaStart);
    }

    private string EndingContext()
    {
        int contextStart = _expected.Length - _suffixLength;
        int contextEnd = Math.Min(contextStart + _contextLength, _expected.Length);
        return _expected.Substring(contextStart, contextEnd - contextStart);
    }

    private string EndingEllipsis()
    {
        return _suffixLength > _contextLength ? ELLIPSIS : "";
    }
    
}
