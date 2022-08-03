using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyEventTrackerOG.Models.Location;
using MyEventTrackerOG.Services;
using System.Security.Claims;

namespace MyEventTrackerOG.WebMVC.Controllers
{
    public class LocationController : Controller
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        private Guid GetUserId()
        {
            var userIdClaim = User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;
            if (userIdClaim == null) return default;
            return Guid.Parse(userIdClaim);
        }


        private bool SetUserIdIS()
        {
            var userId = GetUserId();
            if (userId == null) return false;


            _locationService.SetUserId(userId);
            return true;
        }

        public IActionResult Index()
        {
            if (!SetUserIdIS()) return Unauthorized();

            var locations = _locationService.GetAllLocations();
            return View(locations.ToList());
        }

        public ActionResult Details(int id)
        {
            if (SetUserIdIS()) return Unauthorized();
            var model = _locationService.GetLocationById(id);
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LocationCreate model)
        {
            if (!SetUserIdIS()) return Unauthorized();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (_locationService.CreateLocation(model))
            {
                TempData["SaveResult"] = "Your Location was created!";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Location could not be created.");
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            if (!SetUserIdIS()) return Unauthorized();

            var detail = _locationService.GetLocationById(id);
            var model = new LocationEdit()
            {
                LocationId = detail.LocationId,
                LocationName = detail.LocationName,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, LocationEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.LocationId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            if (!SetUserIdIS()) return Unauthorized();
            if (_locationService.UpdateLocation(model))
            {
                TempData["SaveResult"] = "Your category was updated.";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Your category could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            if (!SetUserIdIS()) return Unauthorized();

            var model = _locationService.GetLocationById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            if (!SetUserIdIS()) return Unauthorized();
            _locationService.DeleteLocation(id);
            TempData["SaveResult"] = "Your note was deleted!";
            return RedirectToAction(nameof(Index));
        }

    }
}
