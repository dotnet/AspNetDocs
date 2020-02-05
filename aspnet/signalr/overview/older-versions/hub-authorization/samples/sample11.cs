class Program
{
    static void Main(string[] args)
    {
        var connection = new HubConnection("https://www.contoso.com/");
        connection.AddClientCertificate(X509Certificate.CreateFromCertFile("MyCert.cer"));
        connection.Start().Wait();
    }
}