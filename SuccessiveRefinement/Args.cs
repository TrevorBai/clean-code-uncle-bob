using System.Collections.Generic;

public class Args
{
    private string _schema;
    private List<string> _argsList;
    private IEnumerator<string> _argsIterator;  // shared cursor
    private bool _valid = true;
    private ISet<char> _unexpectedArguments = new SortedSet<char>();
    private IDictionary<char, ArgumentMarshaller> _marshallers = new Dictionary<char, ArgumentMarshaller>();
    private ISet<char> _argsFound = new HashSet<char>();
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
        _argsList = args.ToList();
        _valid = Parse();
    }

    private bool Parse()
    {
        if (_schema.Length == 0 && _argsList.Count == 0) return true;
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
        if (!_marshallers.ContainsKey(elementId))
            _marshallers.Add(elementId, new BoolArgumentMarshaller());
    }
    
    private void ParseStringSchemaElement(char elementId)
    {
        if (!_marshallers.ContainsKey(elementId))
            _marshallers.Add(elementId, new StringArgumentMarshaller());
    }

    private void ParseIntegerSchemaElement(char elementId)
    { 
        if (!_marshallers.ContainsKey(elementId))
            _marshallers.Add(elementId, new IntegerArgumentMarshaller());
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
        _argsIterator = _argsList.GetEnumerator();
        while (_argsIterator.MoveNext())
        {
            string arg = _argsIterator.Current;
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
        try
        {
            if (m is BoolArgumentMarshaller)
                SetBoolArg(m);
            else if (m is StringArgumentMarshaller)
                SetStringArg(m);
            else if (m is IntegerArgumentMarshaller)
                SetIntArg(m);
            else
                return false;       
        }
        catch (ArgsException e)
        {
            _valid = false;
            _errorArgumentId = argChar;
            throw e;         
        }   
        return true;
    }

    private void SetBoolArg(ArgumentMarshaller m)
    {
        try
        {
            m.Set("true");        
        }
        catch (ArgsException e)
        {          
        }
    }
    
    private void SetStringArg(ArgumentMarshaller m)
    {
        try
        {
            _argsIterator.MoveNext();     
            m.Set(_argsIterator.Current);
        }
        catch (InvalidOperationException e)
        {
            _errorCode = ErrorCode.MISSING_INTEGER;
            throw new ArgsException();
        }
    }

    private void SetIntArg(ArgumentMarshaller m)
    {
        string parameter = null;        
        try
        {
            _argsIterator.MoveNext();            
            parameter = _argsIterator.Current;
            m.Set(parameter);
        }
        catch (InvalidOperationException e)
        {
            _errorCode = ErrorCode.MISSING_INTEGER;
            throw new ArgsException();
        }
        catch (ArgsException e)
        {
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
                return string.Format("Argument {0} expects an integer but was {1}.", _errorArgumentId, _errorParameter);
            case MISSING_INTEGER:
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
        var am = _marshallers[arg];
        try
        {
            return am == null ? string.Empty : (string)am.Get();
        }
        catch (InvalidCastException e)
        {
            return string.Empty;
        }
    }

    public int GetInt(char arg) 
    { 
        var am = _marshallers[arg];
        try
        {
            return am == null ? 0 : (int)am.Get(); 
        }
        catch (Exception e)
        {
            return 0;
        }      
    }

    public bool GetBool(char arg) 
    {
        var am = _marshallers[arg];
        var b = false;
        try
        {
            b = am != null && (bool)am.Get()
        }
        catch (InvalidCastException e)
        {
            b = false;
        }  
        return b;
    }

    public bool Has(char arg) { return _argsFound.Contains(arg); }

    public bool IsValid() { return _valid; }

    private class ArgsException : Exception
    {


        
    }
    
}
