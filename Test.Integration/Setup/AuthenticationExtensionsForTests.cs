using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;

namespace Test.Integration.Setup;

internal static class AuthenticationExtensionsForTests
{
    public static IServiceCollection InjectSynchronousTokenSigningKey(
        this IServiceCollection services)
    {
            return services.PostConfigureAll<JwtBearerOptions>(options =>
            {
                // Replace Signing Key for token validation with our synchronous key
                options.TokenValidationParameters.IssuerSigningKey = AccessTokenBuilder.GetSigningKey();
            });
    }

    public static HttpClient WithAuthenticatedUser(this HttpClient client, string accessToken)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        return client;
    }
}