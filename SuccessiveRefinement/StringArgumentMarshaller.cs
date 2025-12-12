private class StringArgumentMarshaller : ArgumentMarshaller
{
    private string _stringValue = "";

    public override void Set(IEnumerator<string> argsIterator) 
    {
        try
        {
            argsIterator.MoveNext();     
            _stringValue = _argsIterator.Current;
        }
        catch (InvalidOperationException e)
        {
            _errorCode = ErrorCode.MISSING_STRING;
            throw new ArgsException();
        }
    }

    public override void Set(string s) { }

    public override object Get() { return _stringValue; }
}
