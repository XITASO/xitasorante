using Microsoft.Extensions.DependencyInjection;

namespace DomainServices;

public static class ServiceRegistry
{
    public static IServiceCollection AddXitasoRanteDomainServices(this IServiceCollection services)
    {
        return services.AddTransient<IRecipeService, RecipeService>();
    }
}