private abstract class ArgumentMarshaller
{
    public abstract void Set(IEnumerator<string> argsIterator);
    public abstract void Set(string s);
    public abstract object Get();
}
