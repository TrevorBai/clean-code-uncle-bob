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
    private int _prefixIndex;
    private int _suffixIndex;

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
        int suffixLength = 1;
        for (; !SuffixOverlapsPrefix(suffixLength); suffixLength++)
        {
            if (CharFromEnd(_expected, suffixLength) != CharFromEnd(_actual, suffixLength)) break;
        }
        _suffixIndex = suffixLength;
    }

    private char CharFromEnd(string s, int i)
    {
        return s[s.Length - i];
    }

    private bool SuffixOverlapsPrefix(int suffixLength)
    {
        return _actual.Length - suffixLength < _prefixIndex ||
            _expected.Length - suffixLength < _prefixIndex;
    }

    private string CompactString(string source)
    {
        string result = DELTA_START + source.Substring(_prefixIndex, source.Length - _suffixIndex + 1) + DELTA_END;
        if (_prefixIndex > 0) result = ComputeCommonPrefix() + result;
        if (_suffixIndex > 0) result = result + ComputeCommonSuffix();
        return result;
    }

    private void FindCommonPrefix()
    {
        _prefixIndex = 0;
        int end = Math.Min(_expected.Length, _actual.Length);
        for (; _prefixIndex < end; _prefixIndex++)
        {
            if (_expected[_prefixIndex] != _actual[_prefixIndex]) break;
        }
    }

    private string ComputeCommonPrefix()
    {
        return (_prefixIndex > _contextLength ? ELLIPSIS : "") + _expected.Substring(Math.Max(0, _prefixIndex - _contextLength), _prefixIndex);       
    }

    private string ComputeCommonSuffix()
    {
        int end = Math.Min(_expected.Length - _suffixIndex + 1 + _contextLength, _expected.Length);
        return _expected.Substring(_expected.Length - _suffixIndex + 1, end) + 
            (_expected.Length - _suffixIndex + 1 < _expected.Length - _contextLength ? ELLIPSIS : "");
    }

    private bool AreStringsEqual() { return _expected.Equals(_actual); }
    
}
