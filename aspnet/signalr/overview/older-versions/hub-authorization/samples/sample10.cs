class Program
{
    static void Main(string[] args)
    {
        var connection = new HubConnection("https://www.contoso.com/");
        connection.Headers.Add("myauthtoken", /* token data */);
        connection.Start().Wait();
    }
}