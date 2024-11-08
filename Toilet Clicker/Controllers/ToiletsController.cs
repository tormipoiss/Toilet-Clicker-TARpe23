using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Toilet_Clicker.Core.Domain;
using Toilet_Clicker.Core.Dto;
using Toilet_Clicker.Core.ServiceInterface;
using Toilet_Clicker.Data;
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

        public ToiletsController(ToiletClickerContext context, IToiletsServices toiletsServices)
        {
            _context = context;
			_toiletsServices = toiletsServices;
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
			return View();
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
	}
}
