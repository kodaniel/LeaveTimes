namespace LeaveTimes.FunctionalTests.ApiEndpoints;

public abstract class ApiFunctionalTests : IDisposable
{
    protected FunctionalTestWebAppFactory _factory;
    protected HttpClient _client;

    [OneTimeSetUp]
    public virtual void OneTimeSetup() => _factory = new FunctionalTestWebAppFactory();

    [SetUp]
    public virtual void Setup() => _client = _factory.CreateClient();

    public virtual void Dispose() => _factory?.Dispose();
}
