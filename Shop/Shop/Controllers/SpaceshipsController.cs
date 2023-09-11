using Microsoft.AspNetCore.Mvc;
using Shop.data;
using Shop.Models.Spaceship;

namespace Shop.Controllers
{
    public class SpaceshipsController : Controller
    {

        private readonly ShopContext _context;
        public SpaceshipsController
            (
            ShopContext context)
        {
            _context = context;
        }

        
        public IActionResult Index()
        {
            var result = _context.Shapeship
                .Select(x => new SpaceShipsIndexViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = x.Type,
                    EnginePower = x.EnginePower,
                    Passengers = x.Passengers,
                });

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
