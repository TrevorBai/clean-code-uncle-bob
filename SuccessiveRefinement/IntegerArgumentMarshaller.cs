private class IntegerArgumentMarshaller : ArgumentMarshaller
{
    private int _integerValue = 0;

    public override void Set(string s) 
    { 
        try
        {
            _integerValue = int.Parse(s);
        }
        catch (FormatException e)
        {
            throw new ArgsException();
        } 
    }

    public override object Get() { return _integerValue; }
}
