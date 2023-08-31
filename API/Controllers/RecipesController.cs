using Core;
using DomainServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class RecipesController : ControllerBase
{
    [HttpGet]
    [RequiredScope("Recipes.Read")]
    public IEnumerable<RecipeOverviewDto> Index([FromServices] IRecipeService recipeService)
    {
        return recipeService.GetRecipes().Select(MapToDto);
    }

    [HttpGet("{name}")]
    [RequiredScope("Recipes.Read")]
    public ActionResult<RecipeOverviewDto> Get(string name, [FromServices] IRecipeService recipeService)
    {
        var result = recipeService.GetByName(name);
        if (result == null)
        {
            return NotFound(name);
        }
        return Ok(MapToDto(result));
    }

    [HttpPost]
    [RequiredScope("Recipes.Modify")]
    public IActionResult Add(IRecipeService.RecipeData recipeData, [FromServices] IRecipeService recipeService)
    {
        try
        {
            recipeService.AddRecipe(recipeData);
            return Created(Url.Action(nameof(Get))!, recipeData.Title);
        }
        catch (KeyNotFoundException e)
        {
            return BadRequest(e.Message);
        }
    }

    private static RecipeOverviewDto MapToDto(Recipe recipe)
    {
        return new RecipeOverviewDto(
            recipe.Dish.Title,
            recipe.Dish.Description,
            recipe.Ingredients.Select(i => i.Name));
    }
}


public record RecipeOverviewDto(string Title, string Description, IEnumerable<string> ingredients);