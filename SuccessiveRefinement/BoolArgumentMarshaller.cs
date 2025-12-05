private class BoolArgumentMarshaller : ArgumentMarshaller
{
    private bool _boolValue = false;

    public override void Set(string s) { _boolValue = true; }

    public override object Get() { return _boolValue; }
}
