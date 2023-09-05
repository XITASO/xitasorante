using System.Net.Http.Json;
using API.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Test.Integration.MenuItems;

public class MenuItemsTests
{
    [Fact]
    public async Task GetMenuItems_ReturnsMenuItems()
    {
        var factory = new WebApplicationFactory<IngredientsController>();
        var client = factory.CreateClient();

        var result = await client.GetFromJsonAsync<IEnumerable<DishDto>>("/MenuItems");

        result.Should().NotBeEmpty();
    }
}