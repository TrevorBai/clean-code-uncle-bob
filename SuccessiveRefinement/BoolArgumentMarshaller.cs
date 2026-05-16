public class BoolArgumentMarshaller : IArgumentMarshaller
{
    private bool _boolValue = false;

    public void Set(IEnumerator<string> argsIterator) { _boolValue = true; }

    public static bool GetValue(IArgumentMarshaller am) 
    {
        if (am != null && am is BoolArgumentMarshaller)
            return ((BoolArgumentMarshaller)am)._boolValue;
        return false;
    }
}
