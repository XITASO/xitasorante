using Core;
using Infrastructure;
using Infrastructure.Persistence;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

builder.Services
    .AddXitasoRanteInfrastructure(builder.Configuration)
    .AddXitasoRanteCoreDomain();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");


if (app.Configuration.GetValue("AddDummyData", defaultValue: false) &&
    !bool.Parse(app.Configuration.GetSection("FeatureToggles")["Database"] ?? "false"));
{
    app.Services.AddDummyData(app.Configuration);
}

app.Run();