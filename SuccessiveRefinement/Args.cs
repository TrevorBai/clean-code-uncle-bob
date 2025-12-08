public class Args
{
    private string _schema;
    private string[] _args;
    private bool _valid = true;
    private ISet<char> _unexpectedArguments = new SortedSet<char>();
    private IDictionary<char, ArgumentMarshaller> _boolArgs = new Dictionary<char, ArgumentMarshaller>();
    private IDictionary<char, ArgumentMarshaller> _stringArgs = new Dictionary<char, ArgumentMarshaller>();
    private IDictionary<char, ArgumentMarshaller> _intArgs = new Dictionary<char, ArgumentMarshaller>();
    private IDictionary<char, ArgumentMarshaller> _marshallers = new Dictionary<char, ArgumentMarshaller>();
    private ISet<char> _argsFound = new HashSet<char>();
    private int _currentArgument;
    private char _errorArgumentId = '\0';
    private string _errorParameter = "TILT";
    private ErrorCode _errorCode = ErrorCode.OK;

    private enum ErrorCode
    {
        OK,
        MISSING_STRING,
        MISSING_INTEGER,
        INVALID_INTEGER,
        UNEXPECTED_ARGUMENT
    }

    public Args(string schema, string[] args)
    {
        _schema = schema;
        _args = args;
        _valid = Parse();
    }

    private bool Parse()
    {
        if (_schema.Length == 0 && _args.Length == 0) return true;
        ParseSchema();
        try 
        {
            ParseArguments();
        }
        catch (ArgsException e)
        {}
        return _valid;
    }

    private bool ParseSchema()
    {
        foreach (var element in _schema.Split(','))
        {
            if (element.Length > 0)
            {
                string trimmedElement = element.Trim();
                ParseSchemaElement(trimmedElement);
            }        
        }
        return true;
    }

    private void ParseSchemaElement(stirng element)
    {
        var elementId = element[0];
        string elementTail = element.Substring(1);
        ValidateSchemaElementId(elementId);
        if (IsBoolSchemaElement(elementTail))
            ParseBoolSchemaElement(elementId);
        else if (IsStringSchemaElement(elementTail))
            ParseStringSchemaElement(elementId);
        else if (IsIntegerSchemaElement(elementTail))
            ParseIntegerSchemaElement(elementId);
        else
        {
            throw new FormatException(string.Format("Argument: {0} has invalid format: {1}.", elementId, elementTail));
        }      
    }

    private void ValidateSchemaElementId(char elementId)
    {
        if (!char.IsLetter(elementId))
            throw new FormatException("Bad character:" + elementId + "in Args format: " + _schema);
    }

    private void ParseBoolSchemaElement(char elementId)
    {
        var m = new BoolArgumentMarshaller();
        if (!_boolArgs.ContainsKey(elementId))
            _boolArgs.Add(elementId, m);
        if (!_marshallers.ContainsKey(elementId))
            _marshallers.Add(elementId, m);
    }
    
    private void ParseStringSchemaElement(char elementId)
    {
        var m = new StringArgumentMarshaller();       
        if (!_stringArgs.ContainsKey(elementId))
            _stringArgs.Add(elementId, m);
        if (!_marshallers.ContainsKey(elementId))
            _marshallers.Add(elementId, m);
    }

    private void ParseIntegerSchemaElement(char elementId)
    {
        var m = new IntegerArgumentMarshaller();       
        if (!_intArgs.ContainsKey(elementId))
            _intArgs.Add(elementId, m);
        if (!_marshallers.ContainsKey(elementId))
            _marshallers.Add(elementId, m);
    }

    private bool IsBoolSchemaElement(string elementTail)
    {
        return elementTail.Length == 0;
    }

    private bool IsStringSchemaElement(string elementTail)
    {
        return elementTail.Equals("*");
    }

    private bool IsIntegerSchemaElement(string elementTail)
    {
        return elementTail.Equals("#");
    }

    private bool ParseArguments()
    {
        for (var currentArgument = 0; currentArgument < _args.Length; currentArgument++)
        {
            var arg = _args[currentArgument];
            ParseArgument(arg);
        }
        return true;
    }

    private void ParseArgument(string arg)
    {
        if (arg.StartsWith("-")) ParseElements(arg);
    }

    private void ParseElements(string arg)
    {
        for (var i = 1; i < arg.Length; i++)
        {
            ParseElement(arg[i]);
        }
    }

    private void ParseElement(char argChar)
    {
        if (SetArgument(argChar))
            _argsFound.Add(argChar);
        else
            _unexpectedArguments.Add(argChar);
            _errorCode = ErrorCode.UNEXPECTED_ARGUMENT;
            _valid = false;
    }

    private bool SetArgument(char argChar)
    {
        var m = _marshallers[argChar];
        if (m is BoolArgumentMarshaller)
            SetBoolArg(argChar);
        else if (m is StringArgumentMarshaller)
            SetStringArg(argChar);
        else if (m is IntegerArgumentMarshaller)
            SetIntArg(argChar);
        else
            return false;
        return true;
    }

    private void SetBoolArg(char argChar)
    {
        if (!_boolArgs.ContainsKey(argChar))
                _boolArgs.Add(argChar, new BoolArgumentMarshaller());
        try
        {
            _boolArgs[argChar].Set("true");          
        }
        catch (ArgsException e)
        {          
        }
    }
    
    private void SetStringArg(char argChar)
    {
        _currentArgument++;
        try
        {
            if (!_stringArgs.ContainsKey(argChar))
                _stringArgs.Add(argChar, new StringArgumentMarshaller());
            _stringArgs[arcChar].Set(_args[_currentArgument]);
        }
        catch (IndexOutOfRangeException e)
        {
            _valid = false;
            _errorArgumentId = argChar;
            _errorCode = ErrorCode.MISSING_INTEGER;
            throw new ArgsException();
        }
    }

    private void SetIntArg(char argChar)
    {
        _currentArgument++;
        string parameter = null;
        try
        {
            parameter = _args[_currentArgument];
            if (!_intArgs.ContainsKey(argChar))
                _intArgs.Add(argChar, new IntegerArgumentMarshaller());
             _intArgs[argChar].Set(int.Parse(parameter));
        }
        catch (IndexOutOfRangeException e)
        {
            _valid = false;
            _errorArgumentId = argChar;
            _errorCode = ErrorCode.MISSING_INTEGER;
            throw new ArgsException();
        }
        catch (ArgsException e)
        {
            _valid = false;
            _errorArgumentId = argChar;
            _errorParameter = parameter;
            _errorCode = ErrorCode.INVALID_INTEGER;
            throw e;
        }   
    }

    public int Cardinality() { return _argsFound.Count; }

    public string Usage()
    {
        if (_schema.Length > 0)
            return "-[" + _schema + "]";
        else
            return string.Empty;        
    }

    public string ErrorMessage()
    {
        switch (_errorCode)
            case OK:
                throw new Exception("TILT: Should not get here.");
            case UNEXPECTED_ARGUMENT:
                return UnexpectedArgumetnMessage();
            case MISSING_STRING:
                return string.Format("Could not find string parameter for {0}.", _errorArgumentId);
            case INVALID_INTEGER:
                return string.Format("Argument {0} expects an integer but was {0}.", _errorArgumentId, _errorParameter);
            case MISSING)INTEGER:
                return string.Format("Could not find integer parameter for {0}.", _errorArgumentId);
        return string.Empty;
    }

    private string UnexpectedArgumentMessage()
    {
        var message = new StringBuilder("Argument (s) -");
        foreach (var c in _unexpectedArguments)
        {
            message.Append(c);
        }
        message.Append(" unexpected.");
        return message.ToString();
    }

    public string GetString(char arg) 
    { 
        var am = _stringArgs[arg];
        return am == null ? string.Empty : (string)am.Get();
    }

    public int GetInt(char arg) 
    { 
        var am = _intArgs[arg];
        return am == null ? 0 : (int)am.Get(); 
    }

    public bool GetBool(char arg) 
    {
        var am = _boolArgs[arg];
        return am != null && (bool)am.Get();
    }

    public bool Has(char arg) { return _argsFound.Contains(arg); }

    public bool IsValid() { return _valid; }

    private class ArgsException : Exception
    {


        
    }
    
}
