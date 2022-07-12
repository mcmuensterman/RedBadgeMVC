using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyEventTrackerOG.Models.MyEvent;

namespace MyEventTrackerOG.WebMVC.Controllers
{
    [Authorize]
    public class MyEventController : Controller
    {
        public IActionResult Index()
        {
            var model = new MyEventListItem[0];
            return View(model);
        }
    }
}
