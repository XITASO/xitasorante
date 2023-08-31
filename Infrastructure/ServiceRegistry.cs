using Core;
using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceRegistry
{
    public static IServiceCollection AddXitasoRanteInfrastructure(this IServiceCollection services)
    {
        var singleInMemoryRecipeStore = new InMemoryRecipeStore();
        return services
            .AddSingleton<IInventory, InMemoryInventory>()
            .AddSingleton<IRecipeProvider>(singleInMemoryRecipeStore)
            .AddSingleton<IMenu>(singleInMemoryRecipeStore)
            .AddSingleton<IOrderRepository, InMemoryOrderManagement>();
    }
}