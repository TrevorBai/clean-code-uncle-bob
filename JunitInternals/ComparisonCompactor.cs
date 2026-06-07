public class ComparisonCompactor
{
    private static const string ELLIPSIS = "...";
    private static const string DELTA_END = "]";
    private static const string DELTA_START = "[";

    private int _contextLength;
    private string _expected;
    private string _actual;
    private string _compactExpected;
    private string _compactActual; 
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
        if (CanBeCompacted())
        {
            CompactExpectedAndActual();
            return Assert.Format(message, _compactExpected, _compactActual);
        }
        return Assert.Format(message, _expected, _actual);
    }

    private bool CanBeCompacted()
    {
        return _expected != null && _actual != null && !AreStringsEqual();
    }

    private void CompactExpectedAndActual()
    {
        FindCommonPrefixAndSuffix();       
        _compactExpected = CompactString(_expected);
        _compactActual = CompactString(_actual);        
    }

    private void FindCommonPrefixAndSuffix()
    {
        FindCommonPrefix();
        _suffixLength = 0;
        for (; !SuffixOverlapsPrefix(_suffixLength); _suffixLength++)
        {
            if (CharFromEnd(_expected, _suffixLength) != CharFromEnd(_actual, _suffixLength)) break;
        }
    }

    private char CharFromEnd(string s, int i)
    {
        return s[s.Length - i - 1];
    }

    private bool SuffixOverlapsPrefix(int suffixLength)
    {
        return _actual.Length - suffixLength <= _prefixLength ||
            _expected.Length - suffixLength <= _prefixLength;
    }

    private string CompactString(string source)
    {
        string result = DELTA_START + source.Substring(_prefixLength, source.Length - _suffixLength) + DELTA_END;
        if (_prefixLength > 0) result = ComputeCommonPrefix() + result;
        if (_suffixLength > 0) result = result + ComputeCommonSuffix();
        return result;
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

    private string ComputeCommonPrefix()
    {
        return (_prefixLength > _contextLength ? ELLIPSIS : "") + _expected.Substring(Math.Max(0, _prefixLength - _contextLength), _prefixLength);       
    }

    private string ComputeCommonSuffix()
    {
        int end = Math.Min(_expected.Length - _suffixLength + _contextLength, _expected.Length);
        return _expected.Substring(_expected.Length - _suffixLength, end) + 
            (_expected.Length - _suffixLength < _expected.Length - _contextLength ? ELLIPSIS : "");
    }

    private bool AreStringsEqual() { return _expected.Equals(_actual); }
    
}
