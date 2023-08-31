using Core;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceRegistry
{
    public static IServiceCollection AddXitasoRanteInfrastructure(this IServiceCollection services,
        IConfiguration config)
    {
        var databaseEnabled = bool.Parse(config.GetSection("FeatureToggles")["Database"] ?? "false");
        if (databaseEnabled)
        {
            services.AddDbContextFactory<XitasoranteDBContext>(opt =>
                opt.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            services.AddScoped<IInventory, DatabaseInventory>();
        }
        else
        {
            services.AddSingleton<IInventory, InMemoryInventory>();
        }

        var singleInMemoryRecipeStore = new InMemoryRecipeStore();
        return services
            .AddSingleton<IRecipeProvider>(singleInMemoryRecipeStore)
            .AddSingleton<IMenu>(singleInMemoryRecipeStore)
            .AddSingleton<IOrderRepository, InMemoryOrderManagement>();
    }
}