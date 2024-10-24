using Microsoft.AspNetCore.Mvc;
using Toilet_Clicker.Core.Dto;
using Toilet_Clicker.Core.ServiceInterface;
using Toilet_Clicker.Data;
using Toilet_Clicker.Models.Toilets;

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

		[HttpPost]
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

			if (result != null)
			{
				return RedirectToAction("Index");
			}
		}
	}
}
