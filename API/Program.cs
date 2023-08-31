using Core;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using NewTon.API;

const string corsOrigin = "Access-Control-Allow-Origin";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));
builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.AddAuthorizationToSwaggerUI(builder.Configuration));
builder.Services
    .AddXitasoRanteInfrastructure()
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

if (app.Configuration.GetValue("AddDummyData", defaultValue: false))
{
    app.Services.AddDummyData();
}
app.Run();