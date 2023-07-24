public class UserValidator
{
    private readonly Cryptographer _cryptographer;

    public bool CheckPassword(string userName, string password)
    {
        User user = UserGateway.FindByName(userName);
        if (user != null)
        {
            string codedPhrase = user.GetPhraseEncodedByPassword();
            string phrase = cryptographer.Decrypt(codedPhrase, password);

            if ("Valid Password".Equals(phrase)) 
            {
                /* Red line, why initialize the session here? Doing too much stuff. */              
                // Session.Initialize();
                return true;
            }
        }
        return false;
    }
    
}
