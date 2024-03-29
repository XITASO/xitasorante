﻿using Infrastructure.Persistence;

namespace Test.Integration.Setup;

/**
 * Base class for all tests that run against the API
 *
 * Its constructor runs before every test, its dispose method after every test.
 * See https://xunit.net/docs/shared-context#constructor
 */
[Collection(IntegrationTestCollection.IntegrationTestCollectionDefinition)]
public abstract class ApiTestBase : IDisposable
{
    private readonly ApiTestFixture fixture;
    private HttpClient? httpClient;
    private XitasoranteDBContext? db;
    protected HttpClient Client => httpClient ??= fixture.CreateClient();
    protected XitasoranteDBContext DBContext => db ??= fixture.CreateDbContext();

    protected ApiTestBase(ApiTestFixture fixture)
    {
        this.fixture = fixture;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            httpClient?.Dispose();
            db?.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}