using Microsoft.AspNetCore.Mvc;
using Shop.data;
using Shop.Models.Spaceship;
using ShopCore.Dto;
using ShopCore.ServiceInterface;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace Shop.Controllers
{
    public class SpaceshipsController : Controller
    {

        private readonly ShopContext _context;
        private readonly ISpaceshipServices _spaceshipServices;

        public SpaceshipsController
            (
            ShopContext context,
            ISpaceshipServices spaceshipServices
            )
        {
            _context = context;
            _spaceshipServices = spaceshipServices;
        }

        
        public IActionResult Index()
        {
            var result = _context.Spaceships
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
            SpaceshipsCreateUpdateViewModel spaceship = new SpaceshipsCreateUpdateViewModel();

            return View("CreateUpdate", spaceship);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SpaceshipsCreateUpdateViewModel vm)
        {
            var dto = new SpaceshipDto()
            {
                Id = vm.Id,
                Name = vm.Name,
                Type = vm.Type,
                Passengers = vm.Passengers,
                EnginePower = vm.EnginePower,
                Crew = vm.Crew,
                Company = vm.Company,
                CargoWeight = vm.CargoWeight,
                Files = vm.Files,
                Image = vm.FileToApiViewModels
                    .Select(x => new FileToApiDto
                    {
                        Id = x.Id,
                        ExistingFilePath = x.FilePath,
                        SpaceshipId = x.SpaceshipId,
                    }).ToArray()
            };

            var result = await _spaceshipServices.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var spaceship = await _spaceshipServices.GetAsync(id);

            if (spaceship == null)
            {
                return NotFound();
            }

            var images = await _context.FileToApis
                .Where(x => x.SpaceshipId == id)
                .Select(y => new FileToApiViewModel
                {
                    FilePath = y.ExistingFilePath,
                    Id = y.Id
                }).ToArrayAsync();
            var vm = new SpaceshipDetailsViewModel();


            vm.Id = spaceship.Id;
            vm.Name = spaceship.Name;
            vm.Type = spaceship.Type;
            vm.Passengers = spaceship.Passengers;
            vm.EnginePower = spaceship.EnginePower;
            vm.Crew = spaceship.Crew;
            vm.Company = spaceship.Company;
            vm.CargoWeight = spaceship.CargoWeight;
            vm.CreatedAt = spaceship.CreatedAt;
            vm.ModifiedAt = spaceship.ModifiedAt;
            vm.FileToApiViewModels.AddRange(images);
                
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var spaceship = await _spaceshipServices.GetAsync(id);
            if (spaceship == null)
            {
                return NotFound();
            }

            var vm = new SpaceshipsCreateUpdateViewModel();

            vm.Id = spaceship.Id;
            vm.Name = spaceship.Name;
            vm.Type = spaceship.Type;
            vm.Passengers = spaceship.Passengers;
            vm.EnginePower = spaceship.EnginePower;
            vm.Crew = spaceship.Crew;
            vm.Company = spaceship.Company;
            vm.CargoWeight = spaceship.CargoWeight;
            vm.CreatedAt = spaceship.CreatedAt;
            vm.ModifiedAt = spaceship.ModifiedAt;

            return View("CreateUpdate", vm);

            
        }
        [HttpPost]
        public async Task<IActionResult> Update(SpaceshipsCreateUpdateViewModel vm)
        {
            var dto = new SpaceshipDto()
            {
                Id = vm.Id,
                Name = vm.Name,
                Type = vm.Type,
                Passengers = vm.Passengers,
                EnginePower = vm.EnginePower,
                Crew = vm.Crew,
                Company = vm.Company,
                CargoWeight = vm.CargoWeight,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
            };

            var result = await _spaceshipServices.Update(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var spaceship = await _spaceshipServices.GetAsync(id);

            if (spaceship == null)
            {
                return NotFound();
            }

            var vm = new SpaceshipDeleteViewModel();

            vm.Id = spaceship.Id;
            vm.Name = spaceship.Name;
            vm.Type = spaceship.Type;
            vm.Passengers = spaceship.Passengers;
            vm.EnginePower = spaceship.EnginePower;
            vm.Crew = spaceship.Crew;
            vm.Company = spaceship.Company;
            vm.CargoWeight = spaceship.CargoWeight;
            vm.CreatedAt = spaceship.CreatedAt;
            vm.ModifiedAt = spaceship.ModifiedAt;

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var spaceshipId = await _spaceshipServices.Delete(id);

            if (spaceshipId == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
