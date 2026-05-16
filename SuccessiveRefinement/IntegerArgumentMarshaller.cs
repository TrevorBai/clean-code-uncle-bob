using ArgsException.ErrorCode;

public class IntegerArgumentMarshaller : IArgumentMarshaller
{
    private int _integerValue = 0;

    public void Set(IEnumerator<string> argsIterator) 
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
            throw new ArgsException(MISSING_INTEGER);
        }
        catch (FormatException e)
        {
            throw new ArgsException(INVALID_INTEGER, parameter);
        }
    }

    public static int GetValue(IArgumentMarshaller am)
    {
        if (am != null && am is IntegerArgumentMarshaller)
            return ((IntegerArgumentMarshaller)am)._integerValue;
        return 0;
    }
}
