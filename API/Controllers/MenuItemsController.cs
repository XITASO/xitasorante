using Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class MenuItemsController : ControllerBase
{

    [HttpGet]
    public IEnumerable<DishDto> Index([FromServices] IMenu menu)
    {
        return menu.GetMenu().Select(d => new DishDto(d.Title, d.PriceInEuro, d.Description));
    }
}

public record DishDto(string Title, decimal Eur, string Description);
