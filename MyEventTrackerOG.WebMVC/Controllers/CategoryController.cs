using Microsoft.AspNetCore.Mvc;
using MyEventTrackerOG.Services;
using System.Security.Claims;

namespace MyEventTrackerOG.WebMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
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

            //if everything works from above...
            _categoryService.SetUserId(userId);
            return true;
        }

        public IActionResult Index()
        {
            if (!SetUserIdIS()) return Unauthorized();

            var categories = _categoryService.GetAllCategories();
            return View(categories.ToList());
        }
    }
}
