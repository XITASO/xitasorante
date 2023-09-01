using API.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Test.Integration.Setup;


/**
 * This class builds the ASP.NET Core Test server, you can modify the server with this factory for example to stub
 * services or to change configuration variables.
 *
 * See https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-7.0#customize-webapplicationfactory
 */
public class IntegrationTestWebApplicationFactory : WebApplicationFactory<IngredientsController>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting("AddDummyData", false.ToString());
        builder.UseSetting("ConnectionStrings:DefaultConnection", "Server=127.0.0.1,1433;Database=xitasorante_test;User Id=SA;Password=XITASOTestPasswd!;TrustServerCertificate=True;");
        
        // Stub out the retrieval of JWT token validation keys
        builder.ConfigureServices(services =>
        {
            services.InjectSynchronousTokenSigningKey();
        });

        base.ConfigureWebHost(builder);
    }
}