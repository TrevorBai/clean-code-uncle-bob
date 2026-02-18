using System.Collections.Generic;

public class Args
{
    private List<string> _argsList;

    
    private IDictionary<char, ArgumentMarshaller> _marshallers;
    private ISet<char> _argsFound;
    private IEnumerator<string> _argsIterator;  // shared cursor

    public Args(string schema, string[] args)
    {
        _marshallers = = new Dictionary<char, ArgumentMarshaller>();
        _argsFound = new HashSet<char>();
        
        _argsList = args.ToList();

        ParseSchema(schema);
        ParseArguments();
    }

    private void ParseSchema(string schema)
    {
        foreach (var element in schema.Split(','))
            if (element.Length > 0)
                ParseSchemaElement(element.Trim());    
    }

    private void ParseSchemaElement(stirng element)
    {
        var elementId = element[0];
        string elementTail = element.Substring(1);
        ValidateSchemaElementId(elementId);
        if (elementTail.Length == 0)
            ParseBoolSchemaElement(elementId);
        else if (elementTail.Equals("*"))
            ParseStringSchemaElement(elementId);
        else if (elementTail.Equals("#"))
            ParseIntegerSchemaElement(elementId);
        else if (elementTail.Equals("##"))
            ParseDoubleSchemaElement(elementId);         
        else
            throw new ArgsException(ArgsException.ErrorCode.INVALID_FORMAT, elementId, elementTail));        
    }

    private void ValidateSchemaElementId(char elementId)
    {
        if (!char.IsLetter(elementId))
            throw new ArgsException(ArgsException.ErrorCode.INVALID_ARGUMENT_NAME, elementId, null);
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

    private void ParseDoubleSchemaElement(char elementId)
    { 
        if (!_marshallers.ContainsKey(elementId))
            _marshallers.Add(elementId, new DoubleArgumentMarshaller());
    }

    private void ParseArguments()
    {
        _argsIterator = _argsList.GetEnumerator();
        while (_argsIterator.MoveNext())
        {
            string arg = _argsIterator.Current;
            ParseArgument(arg);
        }
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
            throw new ArgsException(ArgsException.ErrorCode.UNEXPECTED_ARGUMENT, argChar, null);
    }

    private bool SetArgument(char argChar)
    {
        var m = _marshallers[argChar];
        if (m == null) return false;
        try
        {
            m.Set(_argsIterator);
            return true;
        }
        catch (ArgsException e)
        {
            e.SetErrorArgumentId(argChar);
            throw e;
        }
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

    public double GetDouble(char arg)
    {
        var am = _marshallers[arg];
        try
        {
            return am == null ? 0 : (double)am.Get();
        }
        catch (Exception e)
        {
            return 0.0;
        }  
    }

    public bool Has(char arg) { return _argsFound.Contains(arg); }
}
