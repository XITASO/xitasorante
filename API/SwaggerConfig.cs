using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace API;

public static class SwaggerConfig
{
    public static Dictionary<string, string > Scopes = new Dictionary<string, string>() 
    {
         { "api://887a0966-f4aa-439c-ab83-4244b1009ee5/Inventory.Modify", "Modify Inventory"},
         { "api://887a0966-f4aa-439c-ab83-4244b1009ee5/Recipes.Modify", "Modify Recipes"},
         { "api://887a0966-f4aa-439c-ab83-4244b1009ee5/Recipes.Read", "Read Recipes"},
    };

    /// <summary>
    /// Add login button to swagger UI that authenticates against Azure Active Directory
    /// </summary>
    public static void AddAuthorizationToSwaggerUI(this SwaggerGenOptions opts, IConfiguration configuration)
    {
        var instance = configuration["AzureAd:Instance"]?.TrimEnd('/') ??
                       throw new ArgumentException("Configuration AzureAd:Instance missing");
        var tenantId = configuration["AzureAd:TenantId"] ??
                       throw new ArgumentException("Configuration AzureAd:TenantId missing");
        var clientId = configuration["AzureAd:ClientId"] ??
                       throw new ArgumentException("Configuration AzureAd:ClientId missing");
        ;
        

        opts.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            Scheme = "bearer",
            In = ParameterLocation.Header,
            Description = "Login with Azure Active Directory",
            Flows = new OpenApiOAuthFlows
            {
                AuthorizationCode = new OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri($"{instance}/{tenantId}/oauth2/v2.0/authorize"),
                    TokenUrl = new Uri($"{instance}/{tenantId}/oauth2/v2.0/token"),
                    Scopes = Scopes,
                },
            },
        });

        opts.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" },
                    Scheme = "bearer",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string> { clientId }
            }
        });
    }

    public static void ConfigureSwaggerUiAuthentication(this SwaggerUIOptions options, IConfiguration configuration)
    {
        var clientId = configuration["AzureAd:ClientId"] ??
                       throw new ArgumentException("Configuration AzureAd:ClientId missing");
        options.OAuthClientId(clientId);
        options.OAuthUsePkce();
        options.OAuthScopeSeparator(" ");
        options.OAuthScopes(Scopes.Keys.ToArray());
    }
}