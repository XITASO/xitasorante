using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class ServiceRegistry
{
   public static IServiceCollection AddXitasoRanteCoreDomain(this IServiceCollection services)
   {
      return services;
   }

   public static void AddDummyData(this IServiceProvider services)
   {
      var inventory = services.GetRequiredService<IInventory>();
         
      inventory.RegisterIngredient(new Ingredient("Tomato", Unit.Pieces, 30));
      inventory.RegisterIngredient(new Ingredient("Olive Oil", Unit.Liters, 20));
      inventory.RegisterIngredient(new Ingredient("Flour", Unit.Grams, 50_000));
   }
}