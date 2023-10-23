using Microsoft.AspNetCore.Mvc;
using Shop.data;
using Shop.Models.RealEstate;
using ShopCore.Dto;
using ShopCore.ServiceInterface;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Shop.ApplicationServices.Services;


namespace Shop.Controllers
{

    public class RealEstateController : Controller
    {
        private readonly ShopContext _context;
        private readonly IRealEstateServices _realestateServices;
        private readonly IFileServices _fileServices;

        public RealEstateController
            (
            ShopContext context,
            IRealEstateServices realEstatesServices,
            IFileServices fileServices
            
            )
        {
            _context = context;
            _realestateServices = realEstatesServices;
            _fileServices = fileServices;   
        }

        public IActionResult Index()
        {
            var result = _context.RealEstates
                .Select(x => new RealEstateIndexViewModel
                {
                    Id = x.Id,
                    Address = x.Address,
                    SizeSqrM = x.SizeSqrM,
                    RoomCount = x.RoomCount,
                    Floor = x.Floor,
                    BuildingType = x.BuildingType,
                    BuiltInYear = x.BuiltInYear,
                });
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            RealEstateCreateUpdateViewModel realestate = new RealEstateCreateUpdateViewModel();

            return View("CreateUpdate", realestate);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RealEstateCreateUpdateViewModel vm)
        {
            var dto = new RealEstateDto()
            {
                Id = vm.Id,
                Address = vm.Address,
                SizeSqrM = vm.SizeSqrM,
                RoomCount = vm.RoomCount,
                Floor = vm.Floor,
                BuildingType = vm.BuildingType,
                BuiltInYear = vm.BuiltInYear,
                Files = vm.Files,
                Images = vm.Image.Select(x => new FileToDatabaseDto
                {
                    Id= x.ImageId,
                    ImageData = x.ImageData,
                    ImageTitle = x.ImageTitle,
                    RealEstateId = x.RealEstateId,
                }).ToArray()

            };
            var result = await _realestateServices.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var realestate = await _realestateServices.GetAsync(id);

            if (realestate == null)
            {
                return NotFound();
            }

            var photos = await _context.FileToDatabases
                .Where(x => x.RealEstateId == id)
                .Select(y => new ImageToDatabaseViewModel
                {
                    RealEstateId = y.Id,
                    ImageId = y.Id,
                    ImageData = y.ImageData,    
                    ImageTitle = y.ImageTitle,  
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();

            var vm = new RealEstateDetailsViewModel();

            vm.Id = realestate.Id;
            vm.Address = realestate.Address;
            vm.SizeSqrM = realestate.SizeSqrM;
            vm.RoomCount = realestate.RoomCount;
            vm.Floor = realestate.Floor;
            vm.BuildingType = realestate.BuildingType;
            vm.BuiltInYear = realestate.BuiltInYear;
            vm.CreatedAt = realestate.CreatedAt;
            vm.UpdatedAt = realestate.UpdatedAt;
            vm.Image.AddRange(photos);


            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var realestate = await _realestateServices.GetAsync(id);
            if (realestate == null)
            {
                return NotFound();
            }

            var photos = await _context.FileToDatabases
                .Where(x => x.RealEstateId == id)
                .Select(y => new ImageToDatabaseViewModel
                {
                    RealEstateId = y.Id,
                    ImageId = y.Id,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64, {0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();

            var vm = new RealEstateCreateUpdateViewModel();

            vm.Id = realestate.Id;
            vm.Address = realestate.Address;
            vm.SizeSqrM = realestate.SizeSqrM;
            vm.RoomCount = realestate.RoomCount;
            vm.Floor = realestate.Floor;
            vm.BuildingType = realestate.BuildingType;
            vm.BuiltInYear = realestate.BuiltInYear;
            vm.CreatedAt = realestate.CreatedAt;
            vm.UpdatedAt = realestate.UpdatedAt;
            vm.Image.AddRange(photos);

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(RealEstateCreateUpdateViewModel vm)
        {
            var dto = new RealEstateDto()
            {
                Id = vm.Id,
                Address = vm.Address,
                SizeSqrM = vm.SizeSqrM,
                RoomCount = vm.RoomCount,
                Floor = vm.Floor,
                BuildingType = vm.BuildingType,
                BuiltInYear = vm.BuiltInYear,
                CreatedAt = vm.CreatedAt,
                UpdatedAt = vm.UpdatedAt,
                Files = vm.Files,
                Image = vm.Image.Select(x => new FileToDatabaseDto
                {
                    Id = x.ImageId,
                    ImageData = x.ImageData,
                    ImageTitle = x.ImageTitle,
                    RealEstateId = x.RealEstateId,
                }).ToArray()
            };
            var result = await _realestateServices.Update(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var realestate = await _realestateServices.GetAsync(id);

            if (realestate == null)
            {
                return NotFound();
            }

            var photos = await _context.FileToDatabases
                .Where(x => x.RealEstateId == id)
                .Select(y => new ImageToDatabaseViewModel
                {
                    RealEstateId = y.Id,
                    ImageId = y.Id,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();

            var vm = new RealEstateDeleteViewModel();

            vm.Id = realestate.Id;
            vm.Address = realestate.Address;
            vm.SizeSqrM = realestate.SizeSqrM;
            vm.RoomCount = realestate.RoomCount;
            vm.Floor = realestate.Floor;
            vm.BuildingType = realestate.BuildingType;
            vm.BuiltInYear = realestate.BuiltInYear;
            vm.CreatedAt = realestate.CreatedAt;
            vm.UpdatedAt = realestate.UpdatedAt;
            vm.ImageToDatabase.AddRange(photos);

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var realestateId = await _realestateServices.Delete(id);

            if (realestateId == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveImage(ImageToDatabaseViewModel file)
        {
            var dto = new FileToDatabaseDto()
            {
                Id = file.ImageId
            };

            var image = await _fileServices.RemoveImageFromDatabase(dto);

            if (image == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

