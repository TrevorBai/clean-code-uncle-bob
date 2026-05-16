using ArgsException.ErrorCode;

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
            throw new ArgsException(MISSING_DOUBLE);
        }
        catch (FormatException e)
        {
            throw new ArgsException(INVALID_DOUBLE, parameter);
        }    
    }

    public static double GetValue(IArgumentMarshaller am) 
    {
        if (am != null && am is DoubleArgumentMarshaller)
            return ((DoubleArgumentMarshaller)am)._doubleValue;
        return 0;
    }
}
