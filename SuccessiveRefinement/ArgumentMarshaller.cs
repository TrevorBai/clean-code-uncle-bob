private abstract class ArgumentMarshaller
{
    private string _stringValue;
    private int _integerValue;

    public void SetString(string s) { _stringValue = s; }

    public string GetString() { return _stringValue == null ? string.Empty : _stringValue; }

    public void SetInteger(int i) { _integerValue = i; }

    public int GetInteger() { return _integerValue; }

    public abstract void Set(string s);

    public abstract object Get();
}
