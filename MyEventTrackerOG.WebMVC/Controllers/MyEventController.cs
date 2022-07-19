﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        private bool SetUserId()
        {
            var userId = GetUserId();
            if (userId == null) return false;

            _myEventService.SetUserId(userId);
            return true;
        }

        public IActionResult Index()
        {
            if (!SetUserId()) return Unauthorized();

            var notes = _myEventService.GetMyEvents();
            return View(notes.ToList());
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MyEventCreate model)
        {
            if (ModelState.IsValid)
            {
                return View(model);
            }

            if (_myEventService.CreateMyEvent(model))
            {
                TempData["SaveResult"] = "Your event was created!";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Event could not be created.");

            return View(model);
        }

 //       public IActionResult Details(int id)
 //       {
 //          var svc = CreateMyEventService();
 //           var model = svc.GetMyEventById(id);

 //           return View(model);
 //       }
    }
}
