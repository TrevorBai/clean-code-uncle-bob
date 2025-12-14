private abstract class ArgumentMarshaller
{
    public abstract void Set(IEnumerator<string> argsIterator);
    public abstract object Get();
}
