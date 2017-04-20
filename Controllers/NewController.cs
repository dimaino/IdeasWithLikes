using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LoginAndRegisterFinal.Models;
using System.Collections.Generic;
using System.Linq;

namespace LoginAndRegisterFinal.Controllers
{
    public class NewController : Controller
    {
        private static MasterContext _context;
    
        public NewController(MasterContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("new")]
        public IActionResult Index()
        {
            if(HttpContext.Session.GetInt32("CurrUserId") != null)
            {
                User CurrentUser = _context.User.SingleOrDefault(person => person.id == (int)HttpContext.Session.GetInt32("CurrUserId"));
                ViewBag.CurrentUser = CurrentUser;               
                return View("Index");
            }
            TempData["error_list"] = new List<string>() {"You need to login to get to this Page."};
            return RedirectToAction("Index", "LoginRegistration");
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