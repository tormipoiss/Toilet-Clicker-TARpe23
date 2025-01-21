//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Toilet_Clicker.ApplicationServices.Services;
//using Toilet_Clicker.Core.ServiceInterface;
//using Toilet_Clicker.Data;
//using Toilet_Clicker.Models.Locations;
//using Toilet_Clicker.Models.Map;

//namespace Toilet_Clicker.Controllers
//{
//    public class MapController : Controller
//    {
//        private readonly ToiletClickerContext _context;
//        private readonly ILocationsServices _locationsServices;
//        private readonly IFileServices _fileServices;

//        public MapController(ToiletClickerContext context, ILocationsServices locationsServices, IFileServices fileServices)
//        {
//            _context = context;
//            _locationsServices = locationsServices;
//            _fileServices = fileServices;
//        }

//        [HttpGet]
//        public async Task<IActionResult> Index()
//        {
//            //var image = await _context.FilesToDatabase
//            //    .Where(t => t.ImageTitle == "Map_1.jpg")
//            //    .Select(y => new MapImageViewModel
//            //    {
//            //        ImageID = y.ID,
//            //        ImageData = y.ImageData,
//            //        ImageTitle = y.ImageTitle,
//            //        Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
//            //    }).ToArrayAsync();

//            //var vm = new MapIndexViewModel();
//            //vm.Image.AddRange(image);

//            //return View(vm);
//            var location = await _locationsServices.

//            if (location == null)
//            {
//                return NotFound(); // <- TODO; custom partial view with message, location is not located
//            }

//            var images = await _context.FilesToDatabase
//                .Where(t => t.LocationID == id)
//                .Select(y => new LocationImageViewModel
//                {
//                    LocationID = y.ID,
//                    ImageID = y.ID,
//                    ImageData = y.ImageData,
//                    ImageTitle = y.ImageTitle,
//                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
//                }).ToArrayAsync();

//            var vm = new LocationDetailsViewModel();
//            vm.ID = location.ID;
//            vm.LocationName = location.LocationName;
//            vm.LocationType = (Models.Locations.LocationType)location.LocationType;
//            vm.LocationDescription = location.LocationDescription;
//            vm.LocationWasMade = location.LocationWasMade;
//            vm.Image.AddRange(images);

//            return View(vm);
//        }
//    }
//}
