using System.Net;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Test.Integration.Setup;

/**
 * This class defines an XUnit collection fixture that sets up the server and tears it down again. Its constructor runs
 * prior to the first test of the collection. Its dispose method runs after the last test of the collection.
 *
 * For collection fixtures see https://xunit.net/docs/shared-context#collection-fixture
 */
// ReSharper disable once ClassNeverInstantiated.Global
public sealed class ApiTestFixture: IDisposable
{
    private readonly IntegrationTestWebApplicationFactory factory;


    public ApiTestFixture()
    {
        factory = new IntegrationTestWebApplicationFactory();
        using var db = CreateDbContext();
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
    }

    public HttpClient CreateClient() => factory.CreateClient();

    public XitasoranteDBContext CreateDbContext() =>
        factory.Services.GetRequiredService<IDbContextFactory<XitasoranteDBContext>>().CreateDbContext();
   
    
    public void Dispose()
    {
        factory.Dispose();
    }
}


[CollectionDefinition(IntegrationTestCollectionDefinition)]
public class IntegrationTestCollection : ICollectionFixture<ApiTestFixture>
{
    public const string IntegrationTestCollectionDefinition = "Integrationtest collection";
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}