using System.Net.Sockets;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class IngredientsController : ControllerBase
{
    private readonly IInventory inventory;

    public IngredientsController(IInventory inventory)
    {
        this.inventory = inventory;
    }
    
    [HttpGet]
    public IEnumerable<IngredientsOverviewDto> Index()
    {
        return inventory.Ingredients.Select(i => new IngredientsOverviewDto(i.Name, i.Amount));
    }
    
    [HttpGet("{name}")]
    public IngredientsOverviewDto GetByName(string name)
    {
        var ingredient = inventory.GetByName(name);
        return new IngredientsOverviewDto(ingredient.Name, ingredient.Amount);
    }
    
    [HttpPost]
    public void Add(AddIngredientDto ingredientData)
    {
        var ingredient = new Ingredient(ingredientData.Name, ingredientData.Unit, ingredientData.InitialAmount);
        inventory.RegisterIngredient(ingredient);
    }
}

public record IngredientsOverviewDto(string Name, double Amount);
public record AddIngredientDto(string Name, Unit Unit, double InitialAmount);
