using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlackBeltTest2.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace BlackBeltTest2.Controllers
{
    public class BlackBeltController : Controller
    {
        private static MasterContext _context;
    
        public BlackBeltController(MasterContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("new")]
        public IActionResult Index()
        {
            if(HttpContext.Session.GetInt32("CurrUserId") != null)
            {
                ViewBag.CurrentUser = _context.User.SingleOrDefault(person => person.id == (int)HttpContext.Session.GetInt32("CurrUserId"));
                return View("Index");
            }
            TempData["error_list"] = new List<string>() {"You need to login to get to this Page."};
            return RedirectToAction("Index", "LoginRegistration");
        }
        
        [HttpGet]
        [Route("addThing")]
        public IActionResult AddThing()
        {
            if(TempData["Time"] != null)
            {
                ViewBag.Time = TempData["Time"];
            }
            return View("Thing");
        }

        // REGEX (([0-1][0-9])|([2][0-3])):([0-5][0-9])
        [HttpPost]
        [Route("checkTime")]
        public IActionResult CheckTime(TimeSpan timeEntered)
        {
            TempData["Time"] = timeEntered;
            return RedirectToAction("AddThing");
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "LoginRegistration");
        }
    }
}