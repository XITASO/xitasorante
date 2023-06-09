using Core;
using Infrastructure;
using Infrastructure.Persistence;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddTransient<ShoppingCart>();
builder.Services.AddTransient<IOrderProcessor, OrderProcessor>();
builder.Services.AddTransient<ICalculationService, CalculationService>();

var singleInMemoryRecipeStore = new InMemoryRecipeStore();
builder.Services.AddSingleton<IRecipeProvider>(singleInMemoryRecipeStore);
builder.Services.AddSingleton<IMenu>(singleInMemoryRecipeStore);
builder.Services.AddSingleton<IOrderRepository, InMemoryOrderManagement>();

builder.Services.AddMudServices();
builder.Services
    .AddXitasoRanteInfrastructure()
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


if (app.Configuration.GetValue("AddDummyData", defaultValue: false))
{
    app.Services.AddDummyData();
}

app.Run();