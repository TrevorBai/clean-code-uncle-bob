private abstract class ArgumentMarshaller
{
    protected bool BoolValue = false;
    private string _stringValue;
    private int _integerValue;

    public void SetBool(bool value) { BoolValue = value; }

    public bool GetBool() { return BoolValue; }

    public void SetString(string s) { _stringValue = s; }

    public string GetString() { return _stringValue == null ? string.Empty : _stringValue; }

    public void SetInteger(int i) { _integerValue = i; }

    public int GetInteger() { return _integerValue; }

    public abstract void Set(string s);
}
