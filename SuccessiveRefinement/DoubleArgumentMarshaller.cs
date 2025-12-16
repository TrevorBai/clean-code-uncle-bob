private class DoubleArgumentMarshaller : IArgumentMarshaller
{
    private double _doubleValue = 0;

    public void Set(IEnumerator<string> argsIterator)
    {
        string parameter = null;
        try
        {
            argsIterator.MoveNext();            
            parameter = argsIterator.Current;
            _doubleValue = double.Parse(parameter);    
        }
        catch (InvalidOperationException e)
        {
            _errorCode = ErrorCode.MISSING_DOUBLE;
            throw new ArgsException();
        }
        catch (FormatException e)
        {
            _errorParameter = parameter;
            _errorCode = ErrorCode.INVALID_DOUBLE;
            throw new ArgsException();  
        }    
    }

    public object Get() { return _doubleValue; }
}
