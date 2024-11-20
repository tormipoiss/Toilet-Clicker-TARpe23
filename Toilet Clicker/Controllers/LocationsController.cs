using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Toilet_Clicker.ApplicationServices.Services;
using Toilet_Clicker.Core.Domain;
using Toilet_Clicker.Core.Dto;
using Toilet_Clicker.Core.ServiceInterface;
using Toilet_Clicker.Data;
using Toilet_Clicker.Models.Locations;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;

namespace Toilet_Clicker.Controllers
{
	public class LocationsController : Controller
	{
		/*
		 * LocationsController controls all functions for toilets, including, missions.
		 */

		private readonly ToiletClickerContext _context;
		private readonly ILocationsServices _locationsServices;
		private readonly IFileServices _fileServices;

		public LocationsController(ToiletClickerContext context, ILocationsServices locationsServices, IFileServices fileServices)
		{
			_context = context;
			_locationsServices = locationsServices;
			_fileServices = fileServices;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var resultingInventory = _context.Locations
				.OrderByDescending(y => y.LocationWasMade)
				.Select(x => new LocationIndexViewModel
				{
					ID = x.ID,
					LocationName = x.LocationName,
					LocationType = (Models.Locations.LocationType)(Core.Dto.LocationType)x.LocationType,
					LocationDescription = x.LocationDescription,
					LocationWasMade = x.CreatedAt,
				});
			return View(resultingInventory);
		}

		[HttpGet]
		public IActionResult Create()
		{
			LocationCreateViewModel vm = new();
			ViewBag.LocationTypes = new SelectList(Enum.GetValues(typeof(Models.Locations.LocationType)).Cast<Models.Locations.LocationType>());
			return View("Create", vm);
		}

		[HttpPost, ActionName("Create")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(LocationCreateViewModel vm)
		{
			var dto = new LocationDto()
			{
				LocationName = vm.LocationName,
				LocationType = (Core.Dto.LocationType)vm.LocationType,
				LocationDescription = vm.LocationDescription,
				LocationWasMade = vm.LocationWasMade,
				CreatedAt = DateTime.Now,
				Files = vm.Files,
				Image = vm.Image
				.Select(x => new FileToDatabaseDto
				{
					ID = x.ImageID,
					ImageData = x.ImageData,
					ImageTitle = x.ImageTitle,
					LocationID = x.LocationID,
				}).ToArray()
			};
			var result = await _locationsServices.Create(dto);

			if (result == null)
			{
				return RedirectToAction("Index");
			}

			return RedirectToAction("Index", vm);
		}
		[HttpGet]
		public async Task<IActionResult> Details(Guid id /*, Guid ref*/)
		{
			var location = await _locationsServices.DetailsAsync(id);

			if (location == null)
			{
				return NotFound(); // <- TODO; custom partial view with message, location is not located
			}

			var images = await _context.FilesToDatabase
				.Where(t => t.LocationID == id)
				.Select(y => new LocationImageViewModel
				{
					LocationID = y.ID,
					ImageID = y.ID,
					ImageData = y.ImageData,
					ImageTitle = y.ImageTitle,
					Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
				}).ToArrayAsync();

			var vm = new LocationDetailsViewModel();
			vm.ID = location.ID;
			vm.LocationName = location.LocationName;
			vm.LocationType = (Models.Locations.LocationType)location.LocationType;
			vm.LocationDescription = location.LocationDescription;
			vm.LocationWasMade = location.LocationWasMade;
			vm.Image.AddRange(images);

			return View(vm);
		}

		[HttpGet]
		public async Task<IActionResult> Update(Guid id)
		{
			if (id == null) { return NotFound(); }

			var location = await _locationsServices.DetailsAsync(id);
			ViewBag.LocationTypes = new SelectList(Enum.GetValues(typeof(Models.Locations.LocationType)).Cast<Models.Locations.LocationType>());

			if (location == null) { return NotFound(); }

			var images = await _context.FilesToDatabase
				.Where(x => x.LocationID == id)
				.Select(y => new LocationImageViewModel
				{
					LocationID = y.ID,
					ImageID = y.ID,
					ImageData = y.ImageData,
					ImageTitle = y.ImageTitle,
					Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
				}).ToArrayAsync();

			var vm = new LocationCreateViewModel();
			vm.ID = location.ID;
			vm.LocationName = location.LocationName;
			vm.LocationType = (Models.Locations.LocationType)location.LocationType;
			vm.LocationDescription = location.LocationDescription;
			vm.LocationWasMade = location.LocationWasMade;
			vm.CreatedAt = location.CreatedAt;
			vm.Image.AddRange(images);

			return View("Update", vm);
		}
		[HttpPost]
		public async Task<IActionResult> Update(LocationCreateViewModel vm)
		{
			var dto = new LocationDto()
			{
				ID = (Guid)vm.ID,
				LocationName = vm.LocationName,
				LocationType = (Core.Dto.LocationType)vm.LocationType,
				LocationDescription = vm.LocationDescription,
				LocationWasMade = vm.LocationWasMade,
				CreatedAt = DateTime.Now,
				Files = vm.Files,
				Image = vm.Image
				.Select(x => new FileToDatabaseDto
				{
					ID = x.ImageID,
					ImageData = x.ImageData,
					ImageTitle = x.ImageTitle,
					LocationID = x.LocationID,
				}).ToArray()
			};
			var result = await _locationsServices.Update(dto);

			if (result == null) { return RedirectToAction("Index"); }
			return RedirectToAction("Index", vm);
		}
		[HttpGet]
		public async Task<IActionResult> Delete(Guid id)
		{
			if (id == null) { return NotFound(); }

			var location = await _locationsServices.DetailsAsync(id);

			if (location == null) { return NotFound(); };

			var images = await _context.FilesToDatabase
				.Where(x => x.LocationID == id)
				.Select(y => new LocationImageViewModel
				{
					LocationID = y.ID,
					ImageID = y.ID,
					ImageData = y.ImageData,
					ImageTitle = y.ImageTitle,
					Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
				}).ToArrayAsync();
			var vm = new LocationDeleteViewModel();

			vm.ID = location.ID;
			vm.LocationName = location.LocationName;
			vm.LocationType = (Models.Locations.LocationType)location.LocationType;
			vm.LocationDescription = location.LocationDescription;
			vm.LocationWasMade = location.LocationWasMade;
			vm.CreatedAt = location.CreatedAt;
			vm.Image.AddRange(images);

			return View(vm);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteConfirmation(Guid id)
		{
			var locationToDelete = await _locationsServices.Delete(id);

			if (locationToDelete == null) { return RedirectToAction("Index"); }

			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> RemoveImage(Guid id)
		{
			var dto = new FileToDatabaseDto()
			{
				ID = id
			};
			var image = await _fileServices.RemoveImageFromDatabase(dto);
			if (image == null) { return RedirectToAction("Index"); }
			return RedirectToAction("Index");
		}
	}
}
