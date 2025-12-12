private class BoolArgumentMarshaller : ArgumentMarshaller
{
    private bool _boolValue = false;

    public override void Set(IEnumerator<string> argsIterator) { _boolValue = true; }

    public override void Set(string s) { }

    public override object Get() { return _boolValue; }
}
