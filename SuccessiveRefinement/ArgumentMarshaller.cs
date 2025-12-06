private abstract class ArgumentMarshaller
{  
    private int _integerValue;

    public void SetInteger(int i) { _integerValue = i; }

    public int GetInteger() { return _integerValue; }

    public abstract void Set(string s);

    public abstract object Get();
}
