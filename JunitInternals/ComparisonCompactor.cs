public class ComparisonCompactor
{
    private static const string ELLIPSIS = "...";
    private static const string DELTA_END = "]";
    private static const string DELTA_START = "[";

    private int _fContextLength;
    private string _fExpected;
    private string _fActual;
    private int _fPrefix;
    private int _fSuffix;

    public ComparisonCompactor(int contextLength, string expected, string actual)
    {
        _fContextLength = contextLength;
        _fExpected = expected;
        _fActual = actual;
    }

    public string Compact(string message)
    {
        if (_fExpected == null || _fActual == null || AreStringsEqual())
            return Assert.Format(message, _fExpected, _fActual);

        FindCommonPrefix();
        FindCommonSuffix();
        string expected = CompactString(_fExpected);
        string actual = CompactString(_fActual);
        return Assert.Format(message, expected, actual);
    }

    private string CompactString(string source)
    {
        string result = DELTA_START + source.Substring(_fPrefix, source.Length - _fSuffix + 1) + DELTA_END;
        if (_fPrefix > 0) result = ComputCommonPrefix() + result;
        if (_fSuffix > 0) result = result + ComputeCommonSuffix();
        return result;
    }

    private void FindCommonPrefix()
    {
        _fPrefix = 0;
        int end = Math.Min(_fExpected.Length, _fActual.Length);
        for (; _fPrefix < end; _fPrefix++)
        {
            if (_fExpected[_fPrefix] != _fActual[_fPrefix]) break;
        }   
    }

    private void FindCommonSuffix()
    {
        int expectedSuffix = _fExpected.Length - 1;
        int actualSuffix = _fActual.Length - 1;
        for (; actualSuffix >= _fPrefix && expectedSuffix >= _fPrefix; actualSuffix--, expectedSuffix--)
        {
            if (_fExpected[_expectedSuffix] != _fActual[_actualSuffix]) break;
        }
        _fSuffix = _fExpected.Length - expectedSuffix;   
    }

    private string ComputeCommonPrefix()
    {
        return (_fPrefix > _fContextLength ? ELLIPSIS : "") + _fExpected.Substring(Math.Max(0, _fPrefix - _fContextLength), _fPrefix);       
    }

    private string ComputeCommonSuffix()
    {
        int end = Math.Min(_fExpected.Length - _fSuffix + 1 + _fContextLength, _fExpected.Length);
        return _fExpected.Substring(_fExpected.Length - _fSuffix + 1, end) + 
            (_fExpected.Length - _fSuffix + 1 < _fExpected.Length - _fContextLength ? ELLIPSIS : "");
    }

    private bool AreStringsEqual() { return _fExpected.Equals(_fActual); }
    
}
