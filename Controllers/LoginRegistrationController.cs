using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BlackBeltTest2.Models;

namespace BlackBeltTest2.Controllers
{
    public class LoginRegistrationController : Controller
    {
        private static MasterContext _context;
    
        public LoginRegistrationController(MasterContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            if(TempData["error_list"] != null)
            {
                ViewBag.Errors = TempData["error_list"];
            }
            HttpContext.Session.Clear();
            return View("Index");
        }

        [HttpPost]
        [Route("register")]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegistrationViewModel model, User newUser)
        {
            List<string> errors = new List<string>();
            if(ModelState.IsValid)
            {
                List<User> allUsers = _context.User.Where(users => users.Email == model.Email).ToList();
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                
                if(allUsers.Count < 1)
                {
                    newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                    _context.User.Add(newUser);
                    _context.SaveChanges();

                    User currentUser = _context.User.SingleOrDefault(user => user.Email == model.Email);
                    HttpContext.Session.SetInt32("CurrUserId", (int)currentUser.id);
                    return RedirectToAction("Index", "BlackBelt");
                }
                else
                {
                    errors.Add("Email already exists in the Database.");
                }
            }
            else
            {
                errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y=>y.Count>0)
                           .Select(z => z[0].ErrorMessage.ToString())
                           .ToList();
            }
            TempData["error_list"] = errors;
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("login")]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            bool passwordCheck = false;
            bool userCheck = false;
            List<string> errors = new List<string>();
            if(ModelState.IsValid)
            {
                User currentUser = _context.User.SingleOrDefault(user => user.Email == model.Email);
                if(currentUser != null && currentUser.Email.Equals(model.Email))
                {
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    if(0 != Hasher.VerifyHashedPassword(currentUser, currentUser.Password, model.Password))
                    {
                        HttpContext.Session.SetInt32("CurrUserId", (int)currentUser.id);
                        return RedirectToAction("Index", "BlackBelt");
                    }
                    passwordCheck = true;
                }
                else
                {
                    userCheck = true;
                }
            }
            errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y=>y.Count>0)
                           .Select(z => z[0].ErrorMessage.ToString())
                           .ToList();
            if(passwordCheck == true)
            {
                errors.Add("Password was incorrect.");
            }
            if(userCheck == true)
            {
                errors.Add("User doesn't exist is the Database.");
            }
            TempData["error_list"] = errors;
            return RedirectToAction("Index");
        }
    }
}
