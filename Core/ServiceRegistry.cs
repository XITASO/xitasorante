using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class ServiceRegistry
{
   public static IServiceCollection AddXitasoRanteCoreDomain(this IServiceCollection services)
   {
      return services.AddSingleton<Inventory>();
   }
}