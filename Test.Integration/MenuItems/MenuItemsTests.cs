using System.Net.Http.Json;
using API.Controllers;
using Core;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Test.Integration.MenuItems;

public class MenuItemsTests
{
    [Fact]
    public async Task GetMenuItems_ReturnsMenuItems()
    {
        await using var factory = new WebApplicationFactory<IngredientsController>();
        var client = factory.CreateClient();

        var result = await client.GetFromJsonAsync<IEnumerable<DishDto>>("/MenuItems");

        result.Should().NotBeEmpty();
    }
    
    [Fact]
    public async Task GetMenuItems_ReturnsSpecificMenuItems()
    {
        await using var factory = new WebApplicationFactory<IngredientsController>();
        factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.Remove(new ServiceDescriptor(typeof(IMenu), typeof(InMemoryRecipeStore),
                    ServiceLifetime.Singleton));
                services.AddSingleton<IMenu, MenuStub>();
            });
        });
        
        var client = factory.CreateClient();

        var result = await client.GetFromJsonAsync<IEnumerable<DishDto>>("/MenuItems");

        result.Should().NotBeEmpty();
    }

    class MenuStub : IMenu
    {
        public IList<Dish> GetMenu()
        {
            return new List<Dish>
            {
                new Dish("Spagetti", 9.5m),
                new Dish("Ice cream", 3.5m)
            };
        }
    }
}
