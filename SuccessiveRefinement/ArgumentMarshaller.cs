private class ArgumentMarshaller
{
    private bool _boolValue = false;
    private string _stringValue;

    public void SetBool(bool value) { _boolValue = value; }

    public bool GetBool() { return _boolValue; }

    public void SetString(string s) { _stringValue = s; }

    public string GetString() { return _stringValue == null ? string.Empty : _stringValue; }
}
