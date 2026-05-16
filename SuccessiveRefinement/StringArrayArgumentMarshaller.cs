using ArgsException.ErrorCode;

public class StringArrayArgumentMarshaller : IArgumentMarshaller {
    private List<String> _strings = new List<String>();

    public void Set(IEnumerator<String> argsIterator) 
    {
        try {
            argsIterator.MoveNext(); 
            _strings.Add(_argsIterator.Current);
        } catch (InvalidOperationException e) {
            throw new ArgsException(MISSING_STRING);
        }
    }

    public static String[] GetValue(IArgumentMarshaller am) {
        if (am != null && am is StringArrayArgumentMarshaller)
            return ((StringArrayArgumentMarshaller)am)._strings.ToArray();
        return new String[0];
    }
}
