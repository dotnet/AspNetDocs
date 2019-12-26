public static class WebApiConfig
{
    public static void Register(HttpConfiguration config)
    {
        config.Services.Replace(typeof(IExceptionHandler), new OopsExceptionHandler());

        // Other configuration code...
    }
}
