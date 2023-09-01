using System.Net;
using System.Net.Http.Json;
using API.Controllers;
using Infrastructure.Persistence;
using Test.Integration.Setup;

namespace Test.Integration.Ingredients;

[Collection(IntegrationTestCollection.IntegrationTestCollectionDefinition)]
public class IngredientsTests : ApiTestBase
{
    public IngredientsTests(ApiTestFixture fixture) : base(fixture)
    {
    }
    
    [Fact]
    public async Task GetIngredients_Returns_All_Ingredients()
    {
        await DBContext.Ingredients.AddAsync(new DatabaseInventory.DBIngredient
            { Name = "Tomatoes", Amount = 5, Unit = Core.Unit.Pieces });
        await DBContext.SaveChangesAsync();
        
        var result = await Client.GetFromJsonAsync<IEnumerable<IngredientsOverviewDto>>("/Ingredients");
        result.Should().BeEquivalentTo(new []
        {
            new IngredientsOverviewDto("Tomatoes", 5)
        });
    }

    [Fact]
    public async Task RegisterIngredients_AddsNewIngredient()
    {
        var ingredientData = new AddIngredientDto("Tomatoes", Core.Unit.Pieces, 5);
        var result = await Client.PostAsJsonAsync("/Ingredients", ingredientData);
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var dbIngredient = DBContext.Ingredients.Single();
        dbIngredient.Name.Should().Be(ingredientData.Name);
        dbIngredient.Unit.Should().Be(ingredientData.Unit);
        dbIngredient.Amount.Should().Be(ingredientData.InitialAmount);
        dbIngredient.Id.Should().NotBeEmpty();
    }
}