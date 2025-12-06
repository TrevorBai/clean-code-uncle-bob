private class StringArgumentMarshaller : ArgumentMarshaller
{
    private string _stringValue = "";

    public override void Set(string s) { _stringValue = s; }

    public override object Get() { return _stringValue; }
}
