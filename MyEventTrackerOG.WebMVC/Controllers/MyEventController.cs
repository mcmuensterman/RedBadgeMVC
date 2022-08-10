using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyEventTrackerOG.Models.Category;
using MyEventTrackerOG.Models.Location;
using MyEventTrackerOG.Models.MyEvent;
using MyEventTrackerOG.Services;
using System.Security.Claims;

namespace MyEventTrackerOG.WebMVC.Controllers
{
    [Authorize]
    public class MyEventController : Controller
    {
        //  NEVER access the data layer from the controller with ntier
        private readonly IMyEventService _myEventService;
        public MyEventController(IMyEventService myEventService)
        {
            _myEventService = myEventService;
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

            _myEventService.SetUserId(userId);
            return true;
        }

        public IActionResult Index()
        {
            if (!SetUserIdIS()) return Unauthorized();

            var myEvents = _myEventService.GetMyEvents();
            return View(myEvents.ToList());
        }


        
        public ActionResult Create()
        {
            ViewBag.CategorySelectList = new SelectList(GetCategoryList(), "CategoryId", "Name");
            ViewBag.LocationSelectList = new SelectList(GetLocationList(), "LocationId", "LocationName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MyEventCreate model)
        {
            if (!SetUserIdIS()) return Unauthorized();

            if (!ModelState.IsValid)
            {
                return View(model);
            }


            if (_myEventService.CreateMyEvent(model))
            {
                TempData["SaveResult"] = "Your Event was created!";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Event could not be created.");

            return View(model);
        }

      

        public ActionResult Details(int id)
        {
            if (!SetUserIdIS()) return Unauthorized();
            
            var model = _myEventService.GetMyEventById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            if (!SetUserIdIS()) return Unauthorized();

            var detail = _myEventService.GetMyEventById(id);
            var model = new MyEventEdit()
            {
                MyEventId = detail.MyEventId,
                EventName = detail.EventName,
                LocationId = detail.LocationId,
                Content = detail.Content
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MyEventEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.MyEventId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            if (!SetUserIdIS()) return Unauthorized();
            if (_myEventService.UpdateMyEvent(model))
            {
                TempData["SaveResult"] = "Your Entry was updated.";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Your Entry could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            if (!SetUserIdIS()) return Unauthorized();

            var model = _myEventService.GetMyEventById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            if (!SetUserIdIS()) return Unauthorized();
            _myEventService.DeleteMyEvent(id);
            TempData["SaveResult"] = "Your Event was deleted!";
            return RedirectToAction(nameof(Index));
        }

        private IEnumerable<CategoryListItem> GetCategoryList()
        {
            if (!SetUserIdIS()) return default;
            return _myEventService.CreateCategoryList();
        }

        private IEnumerable<LocationListItem> GetLocationList()
        {
            if (!SetUserIdIS()) return default;
            return _myEventService.CreateLocationList();
        }

    }
}
