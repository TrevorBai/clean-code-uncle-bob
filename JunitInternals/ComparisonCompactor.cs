public class ComparisonCompactor
{
    private static const string ELLIPSIS = "...";
    private static const string DELTA_END = "]";
    private static const string DELTA_START = "[";

    private int _contextLength;
    private string _expected;
    private string _actual;
    private int _prefix;
    private int _suffix;

    public ComparisonCompactor(int contextLength, string expected, string actual)
    {
        _contextLength = contextLength;
        _expected = expected;
        _actual = actual;
    }

    public string Compact(string message)
    {
        if (ShouldNotCompact())
            return Assert.Format(message, _expected, _actual);

        FindCommonPrefix();
        FindCommonSuffix();
        string compactExpected = CompactString(_expected);
        string compactActual = CompactString(_actual);
        return Assert.Format(message, compactExpected, compactActual);
    }

    private bool ShouldNotCompact()
    {
        return _expected == null || _actual == null || AreStringsEqual();     
    }

    private string CompactString(string source)
    {
        string result = DELTA_START + source.Substring(_prefix, source.Length - _suffix + 1) + DELTA_END;
        if (_prefix > 0) result = ComputCommonPrefix() + result;
        if (_suffix > 0) result = result + ComputeCommonSuffix();
        return result;
    }

    private void FindCommonPrefix()
    {
        _prefix = 0;
        int end = Math.Min(_expected.Length, _actual.Length);
        for (; _prefix < end; _prefix++)
        {
            if (_expected[_prefix] != _actual[_prefix]) break;
        }   
    }

    private void FindCommonSuffix()
    {
        int expectedSuffix = _expected.Length - 1;
        int actualSuffix = _actual.Length - 1;
        for (; actualSuffix >= _prefix && expectedSuffix >= _prefix; actualSuffix--, expectedSuffix--)
        {
            if (_expected[_expectedSuffix] != _actual[_actualSuffix]) break;
        }
        _suffix = _expected.Length - expectedSuffix;   
    }

    private string ComputeCommonPrefix()
    {
        return (_prefix > _contextLength ? ELLIPSIS : "") + _expected.Substring(Math.Max(0, _prefix - _contextLength), _prefix);       
    }

    private string ComputeCommonSuffix()
    {
        int end = Math.Min(_expected.Length - _suffix + 1 + _contextLength, _expected.Length);
        return _expected.Substring(_expected.Length - _suffix + 1, end) + 
            (_expected.Length - _suffix + 1 < _expected.Length - _contextLength ? ELLIPSIS : "");
    }

    private bool AreStringsEqual() { return _expected.Equals(_actual); }
    
}
