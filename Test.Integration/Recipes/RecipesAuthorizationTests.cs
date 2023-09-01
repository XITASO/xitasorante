using System.Net;
using Test.Integration.Setup;

namespace Test.Integration.Recipes;

public class RecipesAuthorizationTests: ApiTestBase
{
    public RecipesAuthorizationTests(ApiTestFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task GetRecipes_ReturnsUnauthorized_OnAnonymousAccess()
    {
        var result = await Client.GetAsync("/Recipes");
        result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task GetRecipes_ReturnsForBidden_OnWrongClaim()
    {
        var accessToken = new AccessTokenBuilder().WithScopes("Arbitrary.Scope").Build();
        var result = await Client
            .WithAuthenticatedUser(accessToken)
            .GetAsync("/Recipes",
                // This option is necessary, otherwise you'll get a HttpRequestException on forbidden
                HttpCompletionOption.ResponseHeadersRead);
        
        result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
    
    [Fact]
    public async Task GetRecipes_ReturnsOK_OnAuthorizedAccess()
    {
        var accessToken = new AccessTokenBuilder().WithScopes("Recipes.Read").Build();
        var result = await Client
            .WithAuthenticatedUser(accessToken)
            .GetAsync("/Recipes");
        
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
