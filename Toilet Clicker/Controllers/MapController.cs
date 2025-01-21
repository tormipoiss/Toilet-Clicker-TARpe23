using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Toilet_Clicker.Core.ServiceInterface;
using Toilet_Clicker.Data;
using Toilet_Clicker.Models.Map;

namespace Toilet_Clicker.Controllers
{
    public class MapController : Controller
    {
        private readonly ToiletClickerContext _context;

        public MapController(ToiletClickerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var image = await _context.FilesToDatabase
                .Where(t => t.ImageTitle == "Map_1.jpg")
                .Select(y => new MapImageViewModel
                {
                    ImageID = y.ID,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();

            var vm = new MapIndexViewModel();
            vm.Image.AddRange(image);

            return View(vm);
        }
    }
}
