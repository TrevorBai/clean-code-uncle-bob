private class BoolArgumentMarshaller : ArgumentMarshaller
{
    public override void Set(string s) { BoolValue = true; }

    public override object Get() { return BoolValue; }
}
