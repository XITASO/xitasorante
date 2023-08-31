using Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class MenuItems : ControllerBase
{
    private IMenu Menu { get; }

    public MenuItems(IMenu menu)
    {
        Menu = menu;
    }
    
    [HttpGet]
    public IEnumerable<DishDto> Index()
    {
        return Menu.GetMenu().Select(d => new DishDto(d.Title, d.PriceInEuro, d.Description));
    }
}

public record DishDto(string Title, decimal Eur, string Description);
