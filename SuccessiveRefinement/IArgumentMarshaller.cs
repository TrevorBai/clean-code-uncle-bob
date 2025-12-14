private interface IArgumentMarshaller
{
    void Set(IEnumerator<string> argsIterator);
    object Get();
}
