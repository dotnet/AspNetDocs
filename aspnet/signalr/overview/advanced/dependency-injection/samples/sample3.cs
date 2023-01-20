// With dependency injection.
class SomeComponent
{
    private readonly ILogger _logger;

    // Inject ILogger into the object.
    public SomeComponent(ILogger logger)
    {
        _logger = logger ?? throw new NullReferenceException(nameof("logger"));
    }

    public void DoSomething()
    {
        try
        {
            _logger.LogMessage($"2 + 2 = {2 + 2}");
        }
        catch(Exception ex)
        {
            _logger.LogError(new EventId(50100, "SignalR_IOC_DI"), ex, ex.Message);
        }
    }
}
