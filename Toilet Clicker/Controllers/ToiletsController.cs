using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Toilet_Clicker.ApplicationServices.Services;
using Toilet_Clicker.Core.Domain;
using Toilet_Clicker.Core.Dto;
using Toilet_Clicker.Core.ServiceInterface;
using Toilet_Clicker.Data;
using Toilet_Clicker.Models.Stories;
using Toilet_Clicker.Models.Toilets;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;

namespace Toilet_Clicker.Controllers
{
	public class ToiletsController : Controller
	{
		/*
		 * ToiletsController controls all functions for toilets, including, missions.
		 */

		private readonly ToiletClickerContext _context;
		private readonly IToiletsServices _toiletsServices;
		private readonly IFileServices _fileServices;

        public ToiletsController(ToiletClickerContext context, IToiletsServices toiletsServices, IFileServices fileServices)
        {
            _context = context;
			_toiletsServices = toiletsServices;
			_fileServices = fileServices;
        }

		[HttpGet]
        public IActionResult Index()
		{
			var resultingInventory = _context.Toilets
				.OrderByDescending(y => y.Score)
				.Select(x => new ToiletIndexViewModel
				{
					ID = x.ID,
					ToiletName = x.ToiletName,
					Score = x.Score,
					Power = x.Power,
					Speed = x.Speed,
					CreatedAt = x.ToiletWasBorn,
				});
			return View(resultingInventory);
		}

		[HttpGet]
		public IActionResult Create()
		{
			ToiletCreateViewModel vm = new();
			return View("Create", vm);
		}

		[HttpPost, ActionName("Create")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ToiletCreateViewModel vm)
		{
			var dto = new ToiletDto()
			{
				ToiletName = vm.ToiletName,
				Power = 1,
				Speed = 1,
				Score = 0,
				ToiletWasBorn = vm.ToiletWasBorn,
				CreatedAt = DateTime.Now,
				Files = vm.Files,
				Image = vm.Image
				.Select(x => new FileToDatabaseDto
				{
					ID = x.ImageID,
					ImageData = x.ImageData,
					ImageTitle = x.ImageTitle,
					ToiletID = x.ToiletID,
				}).ToArray()
			};
			var result = await _toiletsServices.Create(dto);

			if (result == null)
			{
				return RedirectToAction("Index");
			}

			return RedirectToAction("Index", vm);
		}
		[HttpGet]
		public async Task<IActionResult> Details(Guid id /*, Guid ref*/)
		{
			var toilet = await _toiletsServices.DetailsAsync(id);

			if (toilet == null)
			{
				return NotFound(); // <- TODO; custom partial view with message, toilet is not located
			}

			var images = await _context.FilesToDatabase
				.Where(t => t.ToiletID == id)
				.Select(y => new ToiletImageViewModel
				{
					ToiletID = y.ID,
					ImageID = y.ID,
					ImageData = y.ImageData,
					ImageTitle = y.ImageTitle,
					Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
				}).ToArrayAsync();

			var vm = new ToiletDetailsViewModel();
			vm.ID = toilet.ID;
			vm.ToiletName = toilet.ToiletName;
			vm.Power = toilet.Power;
			vm.Speed = toilet.Speed;
			vm.Score = toilet.Score;
			vm.ToiletWasBorn = toilet.ToiletWasBorn;
			vm.Image.AddRange(images);

			return View(vm);
		}

		[HttpGet]
		public async Task<IActionResult> Update(Guid id)
		{
			if (id == null) { return NotFound(); }

			var toilet = await _toiletsServices.DetailsAsync(id);

			if (toilet == null) { return NotFound(); }

			var images = await _context.FilesToDatabase
				.Where(x => x.ToiletID == id)
				.Select(y => new ToiletImageViewModel
				{
					ToiletID = y.ID,
					ImageID = y.ID,
					ImageData = y.ImageData,
					ImageTitle = y.ImageTitle,
					Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
				}).ToArrayAsync();

			var vm = new ToiletCreateViewModel();
			vm.ID = toilet.ID;
			vm.ToiletName = toilet.ToiletName;
			vm.Power = toilet.Power;
			vm.Speed = toilet.Speed;
			vm.Score = toilet.Score;
			vm.ToiletWasBorn = toilet.ToiletWasBorn;
			vm.CreatedAt = toilet.CreatedAt;
			vm.Image.AddRange(images);

			return View("Update", vm);
		}
		[HttpPost]
		public async Task<IActionResult> Update(ToiletCreateViewModel vm)
		{
			var dto = new ToiletDto()
			{
				ID = (Guid)vm.ID,
				ToiletName = vm.ToiletName,
				Power = 1,
				Speed = 1,
				Score = 0,
				ToiletWasBorn = vm.ToiletWasBorn,
				CreatedAt = DateTime.Now,
				Files = vm.Files,
				Image = vm.Image
				.Select(x => new FileToDatabaseDto
				{
					ID = x.ImageID,
					ImageData = x.ImageData,
					ImageTitle = x.ImageTitle,
					ToiletID = x.ToiletID,
				}).ToArray()
			};
			var result = await _toiletsServices.Update(dto);

			if (result == null) { return RedirectToAction("Index"); }
			return RedirectToAction("Index", vm);
		}
		[HttpGet]
		public async Task<IActionResult> Delete(Guid id)
		{
			if (id == null) { return NotFound(); }

			var toilet = await _toiletsServices.DetailsAsync(id);

			if (toilet == null) { return NotFound(); };

			var images = await _context.FilesToDatabase
				.Where(x => x.ToiletID == id)
				.Select(y => new ToiletImageViewModel
				{
					ToiletID = y.ID,
					ImageID = y.ID,
					ImageData = y.ImageData,
					ImageTitle = y.ImageTitle,
					Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
				}).ToArrayAsync();
			var vm = new ToiletDeleteViewModel();

			vm.ID = toilet.ID;
			vm.ToiletName = toilet.ToiletName;
			vm.Power = toilet.Power;
			vm.Speed = toilet.Speed;
			vm.Score = toilet.Score;
			vm.ToiletWasBorn = toilet.ToiletWasBorn;
			vm.CreatedAt = toilet.CreatedAt;
			vm.Image.AddRange(images);

			return View(vm);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteConfirmation(Guid id)
		{
			var toiletToDelete = await _toiletsServices.Delete(id);

			if (toiletToDelete == null) { return RedirectToAction("Index"); }

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

		/*
	
			TOILETOWNERSHIP

		 */

		/// <summary>
		/// player get toilet from story event
		/// </summary>
		/// <param name="vm"></param>
		/// <returns></returns>
        [HttpPost, ActionName("CreateToiletOwnership")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRandomToiletOwnership(ToiletOwnershipFromStoryViewModel vm)
        {
			int RNG = new Random().Next(1, _context.Toilets.Count());

			var sourceToilet = _context.Toilets.OrderByDescending(x => x.ToiletName).Take(RNG);

            var dto = new ToiletOwnershipDto()
            {
                ToiletName = vm.AddedToilet.ToiletName,
                Power = 1,
                Speed = 1,
                Score = 0,
                ToiletWasBorn = vm.AddedToilet.ToiletWasBorn,
                OwnershipCreatedAt = DateTime.Now,
                OwnershipUpdatedAt = DateTime.Now,
                Files = vm.AddedToilet.Files,
                Image = vm.AddedToilet.Image
                //.Select(x => new FileToDatabaseDto
                //{
                //    ID = x.ImageID,
                //    ImageData = x.ImageData,
                //    ImageTitle = x.ImageTitle,
                //    ToiletID = x.ToiletID,
                //}).ToArray()
            };
			//var result = await _storiesServices.Create(dto);
			// STUB, needs storiesservices, a story to utilise, storiescontroller to function

			string result = null;
            if (result == null)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", vm);
        }
    }
}