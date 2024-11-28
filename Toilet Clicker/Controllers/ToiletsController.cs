using System.Globalization;
using System.Text.Json;
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
					PowerPrice = x.PowerPrice,
					Speed = x.Speed,
					SpeedPrice = x.SpeedPrice,
					Location = x.Location,
					ToiletWasBorn = x.CreatedAt,
				});
			ViewBag.mode = "page";
			return View(resultingInventory);
		}

		[HttpGet]
		public IActionResult Create()
		{
			ToiletCreateViewModel vm = new();
			ViewData["LocationID"] = new SelectList(_context.Locations, "ID", "LocationName");
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
				PowerPrice = 1,
				Speed = 1,
				SpeedPrice = 1,
				Score = 0,
				LocationID = vm.LocationID,
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

			ViewData["LocationID"] = new SelectList(_context.Locations, "ID", "LocationName", vm.LocationID);

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
			vm.PowerPrice = toilet.PowerPrice;
			vm.Speed = toilet.Speed;
			vm.SpeedPrice = toilet.SpeedPrice;
			vm.Score = toilet.Score;
			vm.Location = toilet.Location;
			vm.ToiletWasBorn = toilet.ToiletWasBorn;
			vm.Image.AddRange(images);

			return View(vm);
		}

		[HttpGet]
		public async Task<IActionResult> Toilet(Guid id /*, Guid ref*/)
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
			vm.PowerPrice = toilet.PowerPrice;
			vm.Speed = toilet.Speed;
			vm.SpeedPrice = toilet.SpeedPrice;
			vm.Score = toilet.Score;
			vm.Image.AddRange(images);

			return View(vm);
		}

		[HttpPost]
		public async Task<IActionResult> Click(Guid id)
		{
			if (id == Guid.Empty)
			{
				return BadRequest("Invalid toilet ID.");
			}

			var toilet = await _toiletsServices.DetailsAsync(id);
			if (toilet == null)
			{
				return NotFound();
			}

			_context.Entry(toilet).State = EntityState.Detached;

			if (toilet.Power <= 1)
			{
				toilet.Score++;
			}
			else
			{
				if (toilet.Power <= 10)
				{
					toilet.Score += toilet.Power * 2;
				}
				else if (toilet.Power <= 20)
				{
					toilet.Score += toilet.Power * 3;
				}
				else if (toilet.Power <= 30)
				{
					toilet.Score += toilet.Power * 4;
				}
				else if (toilet.Power <= 40)
				{
					toilet.Score += toilet.Power * 6;
				}
				else
				{
					toilet.Score += toilet.Power * 8;
				}
			}

			if (TempData["clickDb"] == null)
			{
				TempData["clickDb"] = JsonSerializer.Serialize(new List<List<string>>());

				var clickDb = JsonSerializer.Deserialize<List<List<string>>>(TempData["clickDb"] as string);
				List<string> temp = new List<string> { toilet.ID.ToString(), "0", DateTime.Now.ToString() };
				clickDb.Add(temp);

				TempData["clickDb"] = JsonSerializer.Serialize(clickDb);
			}
			else
			{
				var clickDb = JsonSerializer.Deserialize<List<List<string>>>(TempData["clickDb"] as string);
				bool foundID = false;
				int i = 0;
				foreach (var temp in clickDb)
				{
					if (temp[0] == toilet.ID.ToString())
					{
						foundID = true;
						if (temp.Count > 4)
						{
							temp.RemoveAt(2);
							temp.Add(DateTime.Now.ToString());
						}
						else
						{
							temp.Add(DateTime.Now.ToString());
						}
						for (int j = 2; j < temp.Count; j++)
						{
							TimeSpan dateAge = (DateTime.Parse(temp[j])).TimeOfDay - (DateTime.Parse(temp[temp.Count - 1])).TimeOfDay;
							double minutes = dateAge.TotalMinutes;
							if (Math.Abs(minutes) > 30)
							{
								temp.RemoveAt(j);
								j--;
							}
						}
						if (temp.Count > 3)
						{
							TimeSpan totalSeconds = (DateTime.Parse(temp[temp.Count - 1])).TimeOfDay - (DateTime.Parse(temp[temp.Count - 2])).TimeOfDay;
							double seconds = Math.Abs(totalSeconds.TotalSeconds);
							string oldDecimalValue = $"0.{temp[1]}";
							seconds += double.Parse(oldDecimalValue, CultureInfo.InvariantCulture);
							if (Int32.TryParse(seconds.ToString(), out int fullSeconds))
							{
								temp[1] = "0";
								toilet.Score += (ulong)fullSeconds * toilet.Speed;
							}
							else
							{
								List<string> secondsList = new List<string>();
								secondsList = seconds.ToString().Split('.').ToList();
								temp[1] = secondsList[1];
								toilet.Score += Convert.ToUInt64(secondsList[0]) * toilet.Speed;
							}
						}
						clickDb[i] = temp;
						TempData["clickDb"] = JsonSerializer.Serialize(clickDb);
						break;
					}
					i++;
				}

				if (!foundID)
				{
					List<string> temp = new List<string>() { toilet.ID.ToString(), "0", DateTime.Now.ToString() };
					clickDb.Add(temp);
					TempData["clickDb"] = JsonSerializer.Serialize(clickDb);
				}
			}

			var dto = new ToiletDto()
			{
				ID = (Guid)toilet.ID,
				ToiletName = toilet.ToiletName,
				Power = toilet.Power,
				PowerPrice = toilet.PowerPrice,
				Speed = toilet.Speed,
				SpeedPrice = toilet.SpeedPrice,
				Score = toilet.Score,
				LocationID = toilet.LocationID,
				ToiletWasBorn = toilet.ToiletWasBorn,
				CreatedAt = toilet.CreatedAt
			};
			var result = await _toiletsServices.Update(dto);

			if (result == null)
			{
				return RedirectToAction("Index");
			}

			return RedirectToAction("Toilet", new { id });
		}

		[HttpPost]
		public async Task<IActionResult> UpgradePower(Guid id)
		{
			if (id == Guid.Empty)
			{
				return BadRequest("Invalid toilet ID.");
			}

			var toilet = await _toiletsServices.DetailsAsync(id);
			if (toilet == null)
			{
				return NotFound();
			}

			_context.Entry(toilet).State = EntityState.Detached;

			if (toilet.Score >= toilet.PowerPrice)
			{
				toilet.Power++;
				toilet.Score -= toilet.PowerPrice;
				if (toilet.PowerPrice < 100)
				{
					toilet.PowerPrice *= 2;
				}
				else if (toilet.PowerPrice < 200)
				{
					toilet.PowerPrice = (ulong)Math.Round(toilet.PowerPrice * 1.7);
				}
				else if (toilet.PowerPrice < 300)
				{
					toilet.PowerPrice = (ulong)Math.Round(toilet.PowerPrice * 1.4);
				}
				else if (toilet.PowerPrice < 600)
				{
					toilet.PowerPrice = (ulong)Math.Round(toilet.PowerPrice * 1.2);
				}
				else
				{
					toilet.PowerPrice = (ulong)Math.Round(toilet.PowerPrice * 1.1);
				}
			}

			var dto = new ToiletDto()
			{
				ID = (Guid)toilet.ID,
				ToiletName = toilet.ToiletName,
				Power = toilet.Power,
				PowerPrice = toilet.PowerPrice,
				Speed = toilet.Speed,
				SpeedPrice = toilet.SpeedPrice,
				Score = toilet.Score,
				LocationID = toilet.LocationID,
				ToiletWasBorn = toilet.ToiletWasBorn,
				CreatedAt = toilet.CreatedAt
			};
			var result = await _toiletsServices.Update(dto);

			if (result == null)
			{
				return RedirectToAction("Index");
			}

			return RedirectToAction("Toilet", new { id });
		}

		[HttpPost]
		public async Task<IActionResult> UpgradeSpeed(Guid id)
		{
			if (id == Guid.Empty)
			{
				return BadRequest("Invalid toilet ID.");
			}

			var toilet = await _toiletsServices.DetailsAsync(id);
			if (toilet == null)
			{
				return NotFound();
			}

			_context.Entry(toilet).State = EntityState.Detached;

			if (toilet.Score >= toilet.SpeedPrice)
			{
				toilet.Speed++;
				toilet.Score -= toilet.SpeedPrice;
				if (toilet.SpeedPrice < 100)
				{
					toilet.SpeedPrice *= 2;
				}
				else if (toilet.SpeedPrice < 200)
				{
					toilet.SpeedPrice = (ulong)Math.Round(toilet.SpeedPrice * 1.7);
				}
				else if (toilet.SpeedPrice < 300)
				{
					toilet.SpeedPrice = (ulong)Math.Round(toilet.SpeedPrice * 1.4);
				}
				else if (toilet.SpeedPrice < 600)
				{
					toilet.SpeedPrice = (ulong)Math.Round(toilet.SpeedPrice * 1.2);
				}
				else
				{
					toilet.SpeedPrice = (ulong)Math.Round(toilet.SpeedPrice * 1.1);
				}
			}

			var dto = new ToiletDto()
			{
				ID = (Guid)toilet.ID,
				ToiletName = toilet.ToiletName,
				Power = toilet.Power,
				PowerPrice = toilet.PowerPrice,
				Speed = toilet.Speed,
				SpeedPrice = toilet.SpeedPrice,
				Score = toilet.Score,
				LocationID = toilet.LocationID,
				ToiletWasBorn = toilet.ToiletWasBorn,
				CreatedAt = toilet.CreatedAt
			};
			var result = await _toiletsServices.Update(dto);

			if (result == null)
			{
				return RedirectToAction("Index");
			}

			return RedirectToAction("Toilet", new { id });
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
			vm.PowerPrice = toilet.PowerPrice;
			vm.Speed = toilet.Speed;
			vm.SpeedPrice = toilet.SpeedPrice;
			vm.Score = toilet.Score;
			vm.LocationID = toilet.LocationID;
			vm.ToiletWasBorn = toilet.ToiletWasBorn;
			vm.CreatedAt = toilet.CreatedAt;
			vm.Image.AddRange(images);

			ViewData["LocationID"] = new SelectList(_context.Locations, "ID", "LocationName", vm.LocationID);

			return View("Update", vm);
		}
		[HttpPost]
		public async Task<IActionResult> Update(ToiletCreateViewModel vm)
		{
			var dto = new ToiletDto()
			{
				ID = (Guid)vm.ID,
				ToiletName = vm.ToiletName,
				Power = vm.Power,
				PowerPrice = vm.PowerPrice,
				Speed = vm.Speed,
				SpeedPrice = vm.SpeedPrice,
				Score = vm.Score,
				LocationID = vm.LocationID,
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

			ViewData["LocationID"] = new SelectList(_context.Locations, "ID", "LocationName", vm.LocationID);

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
			vm.PowerPrice = toilet.PowerPrice;
			vm.Speed = toilet.Speed;
			vm.SpeedPrice = toilet.SpeedPrice;
			vm.Score = toilet.Score;
			vm.Location = toilet.Location;
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
	}
}