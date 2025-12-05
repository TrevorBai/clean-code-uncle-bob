private class ArgumentMarshaller
{
    private bool _boolValue = false;

    public void SetBool(bool value) { _boolValue = value; }

    public bool GetBool() { return _boolValue; }
}
