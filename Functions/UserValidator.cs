public class UserValidator
{
    private readonly Cryptographer _cryptographer;

    public bool CheckPassword(string userName, string password)
    {
        User user = UserGateway.FindByName(userName);
        if (user != null)
        {
            string codedPhrase = user.GetPhraseEncodedByPassword();
            string phrase = _cryptographer.Decrypt(codedPhrase, password);

            if ("Valid Password".Equals(phrase)) 
            {
                /* Red line, why initialize the session here? Doing too much stuff. 
                 * This is called temporal coupling, it's confusing, especially when
                 * hidden as a side effect. */
                // Session.Initialize();
                
                return true;
            }
        }
        return false;
    }
    
}
