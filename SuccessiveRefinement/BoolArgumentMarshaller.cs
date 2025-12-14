private class BoolArgumentMarshaller : IArgumentMarshaller
{
    private bool _boolValue = false;

    public void Set(IEnumerator<string> argsIterator) { _boolValue = true; }

    public object Get() { return _boolValue; }
}
