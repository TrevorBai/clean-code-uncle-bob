private class IntegerArgumentMarshaller : ArgumentMarshaller
{
    private int _integerValue = 0;

    public override void Set(IEnumerator<string> argsIterator) 
    { 
        string parameter = null;        
        try
        {
            argsIterator.MoveNext();            
            parameter = argsIterator.Current;
            _integerValue = int.Parse(parameter);
        }
        catch (InvalidOperationException e)
        {
            _errorCode = ErrorCode.MISSING_INTEGER;
            throw new ArgsException();
        }
        catch (FormatException e)
        {
            _errorParameter = parameter;
            _errorCode = ErrorCode.INVALID_INTEGER;
            throw new ArgsException();
        }
    }

    public override object Get() { return _integerValue; }
}
