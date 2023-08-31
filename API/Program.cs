using API;
using Core;
using DomainServices;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

const string corsOrigin = "Access-Control-Allow-Origin";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(jwtOptions =>
    {
        jwtOptions.Audience = "887a0966-f4aa-439c-ab83-4244b1009ee5";
    }, 
    msIdentityOptions => builder.Configuration.GetSection("AzureAd").Bind(msIdentityOptions)
   );
builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.AddAuthorizationToSwaggerUI(builder.Configuration));
builder.Services
    .AddXitasoRanteInfrastructure(builder.Configuration)
    .AddXitasoRanteDomainServices()
    .AddXitasoRanteCoreDomain();


builder.Services.AddCors((options =>
{
    options.AddPolicy(name: corsOrigin,
        policy =>
        {
            policy.WithOrigins("*")
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
}));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.ConfigureSwaggerUiAuthentication(app.Configuration));
}

app.UseHttpsRedirection();

app.UseCors(corsOrigin);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

if (app.Configuration.GetValue("AddDummyData", defaultValue: false) &&
    !bool.Parse(app.Configuration.GetSection("FeatureToggles")["Database"] ?? "false"))
{
    app.Services.AddDummyData(app.Configuration);
}
app.Run();