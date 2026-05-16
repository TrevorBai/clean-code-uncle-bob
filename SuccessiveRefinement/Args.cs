using System.Collections.Generic;
using ArgsException.ErrorCode;

public class Args
{
    private IDictionary<char, ArgumentMarshaller> _marshallers;
    private ISet<char> _argsFound;
    private IEnumerator<string> _argsIterator; // shared cursor

    public Args(string schema, string[] args)
    {
        _marshallers = = new Dictionary<char, ArgumentMarshaller>();
        _argsFound = new HashSet<char>();
        
        ParseSchema(schema);
        ParseArgumentStrings(args.ToList());
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
        else if (elementTail.Equals("[*]"))
            ParseStringArraySchemaElement(elementId);
        else
            throw new ArgsException(INVALID_FORMAT, elementId, elementTail));        
    }

    private void ValidateSchemaElementId(char elementId)
    {
        if (!char.IsLetter(elementId))
            throw new ArgsException(INVALID_ARGUMENT_NAME, elementId, null);
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

    private void ParseStringArraySchemaElement(char elementId) 
    {
        if (!_marshallers.ContainsKey(elementId))
            _marshallers.Add(elementId, new StringArrayArgumentMarshaller());
    }

    private void ParseArgumentStrings(List<string> argsList)
    {
        _argsIterator = _argsList.GetEnumerator();
        while (_argsIterator.MoveNext())
        {
            string arg = _argsIterator.Current;
            if (arg.StartsWith("-")) 
            {
                ParseArgumentCharacters(arg.Substring(1));
            } else
            {
                break;
            }
        }
    }

    private void ParseArgumentCharacters(string arg)
    {
        for (var i = 0; i < arg.Length; i++)
        {
            ParseArgumentCharacter(arg[i]);
        }
    }

    private void ParseArgumentCharacter(char argChar)
    {
        var m = _marshallers[argChar];
        if (m == null)
            throw new ArgsException(UNEXPECTED_ARGUMENT, argChar, null);
        else
        {
            _argsFound.Add(argChar);
            try
            {
                m.Set(_argsIterator);
            }
            catch (ArgsException e)
            {
                e.SetErrorArgumentId(argChar);
                throw e;
            }       
        }
    }

    public string GetString(char arg) 
    { 
        return StringArgumentMarshaller.GetValue(_marshallers[arg]);
    }

    public int GetInt(char arg) 
    {
        return IntegerArgumentMarshaller.GetValue(_marshallers[arg]);    
    }

    public bool GetBool(char arg) 
    {
        return BoolArgumentMarshaller.GetValue(_marshallers[arg]);
    }

    public double GetDouble(char arg)
    {
        return DoubleArgumentMarshaller.GetValue(_marshallers[arg]);
    }

    public string[] GetStringArray(char arg)
    {
        return StringArrayArgumentMarshaller.GetValue(_marshallers[arg]);
    }

    public bool Has(char arg) { return _argsFound.Contains(arg); }
}
