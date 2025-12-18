private class StringArgumentMarshaller : IArgumentMarshaller
{
    private string _stringValue = "";

    public void Set(IEnumerator<string> argsIterator) 
    {
        try
        {
            argsIterator.MoveNext();     
            _stringValue = _argsIterator.Current;
        }
        catch (InvalidOperationException e)
        {
            _errorCode = ArgsException.ErrorCode.MISSING_STRING;
            throw new ArgsException();
        }
    }

    public object Get() { return _stringValue; }
}
