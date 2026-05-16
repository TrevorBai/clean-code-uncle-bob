using ArgsException.ErrorCode;

public class StringArgumentMarshaller : IArgumentMarshaller
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
            throw new ArgsException(MISSING_STRING);
        }
    }

    public static string GetValue(IArgumentMarshaller am)
    {
        if (am != null && am is StringArgumentMarshaller)
            return ((StringArgumentMarshaller)am)._stringValue;
        return string.Empty;
    }
}
