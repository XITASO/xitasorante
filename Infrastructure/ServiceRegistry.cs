using Core;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceRegistry
{
   public static IServiceCollection AddXitasoRanteInfrastructure(this IServiceCollection services )
   {
      return services.AddSingleton<IInventory, InMemoryInventory>();
   }
}