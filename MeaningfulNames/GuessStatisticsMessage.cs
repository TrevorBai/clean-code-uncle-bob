public class GuessStatisticsMessage
{
    private readonly string _number;
    private readonly string _verb;
    private readonly string _pluralModifier;
    
    public string Make(char candidate, int count)
    {
        CreatePluralDependentMessageParts(count);
        return string.Format("There {0} {1} {2}{3}", _verb, _number, candidate, _pluralModifier);
    }

    private void CreatePluralDependentMessageParts(int count)
    {
        if (count == 0) {
            ThereAreNoLetters();
        } else if (count == 1) {
            ThereIsOneLetter();
        } else {
            ThereAreManyLetters(count);
        }   
    }
    
    private void ThereAreManyLetters(int count)
    {
        _number = count.ToString();
        _verb = "are";
        _pluralModifier = "s";
    }
    
    private void ThereIsOneLetter()
    {
        _number = "1";
        _verb = "is";
        _pluralModifier = "";
    }
    
    private void ThereAreNoLetters()
    {
        _number = "no";
        _verb = "are";
        _pluralModifier = "s";
    }

}
