class Program
{
    static void Main(string[] args)
    {
        var connection = new HubConnection("https://www.contoso.com/");
        connection.Credentials = CredentialCache.DefaultCredentials;
        connection.Start().Wait();
    }
}