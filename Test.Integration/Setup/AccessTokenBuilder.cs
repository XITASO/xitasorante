using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Test.Integration.Setup
{
    public class AccessTokenBuilder
    {
        public static readonly Guid UserId = Guid.Parse("E34D24A8-6C52-452D-AE1C-B987AD5AC92A");

        private readonly IDictionary<string, string> claims = DefaultClaims();
        private string issuer = "https://login.microsoftonline.com/8ebc8a73-ff9c-4d16-8c3b-e47b53a998f6/v2.0"; // issuer in .well-known/openid-configuration
        private string audience = "887a0966-f4aa-439c-ab83-4244b1009ee5"; // same as jwtOptions.Audience in Program.ts
        private DateTime? notBefore = DateTime.UtcNow;
        private DateTime? expires = DateTime.UtcNow.AddHours(2);
        private DateTime? issuedAt = DateTime.UtcNow;

        public string Build()
        {
            var credentials = GetSigningCredentials();
            var jwtHeader = new JwtHeader(credentials);
            var claimsParameter = claims.Select(dictEntry => new Claim(dictEntry.Key, dictEntry.Value));
            var jwtPayload = new JwtPayload(issuer, audience, claimsParameter, notBefore, expires, issuedAt);
            var token = new JwtSecurityToken(jwtHeader, jwtPayload);

            var handler = new JwtSecurityTokenHandler();
            var encodedToken = handler.WriteToken(token);

            return encodedToken;
        }

        public AccessTokenBuilder WithName(string name)
        {
            claims["name"] = name;
            return this;
        }

        public AccessTokenBuilder WithIssuer(string value)
        {
            issuer = value;
            return this;
        }

        public AccessTokenBuilder WithAudience(string value)
        {
            audience = value;
            return this;
        }

        public AccessTokenBuilder NotBefore(DateTime? value)
        {
            notBefore = value;
            return this;
        }

        public AccessTokenBuilder Expires(DateTime? value)
        {
            expires = value;
            return this;
        }

        public AccessTokenBuilder IssuedAt(DateTime? value)
        {
            issuedAt = value;
            return this;
        }

        public AccessTokenBuilder WithEMail(string value)
        {
            claims[ClaimTypes.Email] = value;
            return this;
        }


        public AccessTokenBuilder WithId(Guid value)
        {
            claims["http://schemas.microsoft.com/identity/claims/objectidentifier"] = value.ToString();
            return this;
        }
        
        public AccessTokenBuilder WithScopes(params string[] scopes)
        {
            claims["scp"] = string.Join(' ', scopes);
            return this;
        }

        private static IDictionary<string, string> DefaultClaims()
        {
            return new Dictionary<string, string>
            {
                { "name", "Test User" },
                { "http://schemas.microsoft.com/identity/claims/objectidentifier", UserId.ToString() },
            };
        }

        public static SecurityKey GetSigningKey()
        {
            using var content = Assembly.GetAssembly(typeof(AccessTokenBuilder))
                ?.GetManifestResourceStream("Test.Integration.Setup.synchronous-token-signing-keys.json");
            using var reader = new StreamReader(content!);
            var keysJson = reader.ReadToEnd();
            var keySet = JsonWebKeySet.Create(keysJson);
            var key = keySet.GetSigningKeys().Single();
            return key!;
        }

        private static SigningCredentials GetSigningCredentials()
        {
            var key = GetSigningKey();
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            return credentials;
        }

    }
}