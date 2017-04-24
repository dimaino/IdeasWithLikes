using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlackBeltTest2.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

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
        [Route("bright_ideas")]
        public IActionResult Index()
        {
            if(HttpContext.Session.GetInt32("CurrUserId") != null)
            {
                ViewBag.CurrentUser = _context.User.SingleOrDefault(person => person.UserId == (int)HttpContext.Session.GetInt32("CurrUserId"));
                // Order By
                List<Idea> allIdeas = _context.Idea.Include(interest => interest.User).Include(inter => inter.IdeasLikes).OrderByDescending(like => like.IdeasLikes.Count).ToList();
                ViewBag.AllIdeas = allIdeas;
                return View("Index");
            }
            TempData["error_list"] = new List<string>() {"You need to login to get to this Page."};
            ViewBag.NoIdea = TempData["NoIdea"];
            return RedirectToAction("Index", "LoginRegistration");
        }
        
        [HttpPost]
        [Route("addIdea")]
        public IActionResult AddIdea(string Content)
        {
            if(Content != null)
            {
                Idea newIdea = new Idea{
                    Content = Content,
                    UserId = (int)HttpContext.Session.GetInt32("CurrUserId"),
                };
                _context.Add(newIdea);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            TempData["NoIdea"] = new List<string>{"You have to have an idea before posting."};
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("addLike/{id}")]
        public IActionResult AddLike(int id)
        {
            Like checkLikes = _context.Like.Where(l => l.UserId == (int)HttpContext.Session.GetInt32("CurrUserId") && l.IdeaId == id).SingleOrDefault();
            if(checkLikes == null)
            {
                Like newLike = new Like{
                UserId = (int)HttpContext.Session.GetInt32("CurrUserId"),
                IdeaId = id,
                };
                _context.Add(newLike);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("deleteLike/{id}")]
        public IActionResult DeleteLike(int id)
        {
            List<Like> removeLikes = _context.Like.Where(idea => idea.IdeaId == id).ToList();
            foreach(var like in removeLikes)
            {
                _context.Remove(like);
            }
            _context.SaveChanges();
            Idea removeIdea = _context.Idea.Where(idea => idea.IdeaId == id).SingleOrDefault();
            _context.Remove(removeIdea);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("users/{id}")]
        public IActionResult ShowUser(int id)
        {
            if(HttpContext.Session.GetInt32("CurrUserId") != null)
            {
            User clickedUser = _context.User.Where(user => user.UserId == id).Include(ideas => ideas.Ideas).Include(likes => likes.UserLikes).SingleOrDefault();
            ViewBag.ClickedUser = clickedUser;
            return View("User");
            }
            TempData["error_list"] = new List<string>() {"You need to login to get to this Page."};
            ViewBag.NoIdea = TempData["NoIdea"];
            return RedirectToAction("Index", "LoginRegistration");
        }

        [HttpGet]
        [Route("bright_ideas/{id}")]
        public IActionResult ShowIdea(int id)
        {
            if(HttpContext.Session.GetInt32("CurrUserId") != null)
            {
            //User clickedUser = _context.User.Where(user => user.UserId == id).Include(ideas => ideas.Ideas).Include(likes => likes.UserLikes).SingleOrDefault();
            //ViewBag.ClickedUser = clickedUser;
            List<Idea> clickedIdea = _context.Idea.Where(idea2 => idea2.IdeaId == id).Include(likes => likes.IdeasLikes).Include(user => user.IdeasLikes).ThenInclude(u => u.User).Include(er => er.User).ToList();
            ViewBag.ClickedIdea = clickedIdea;
            Idea idea = _context.Idea.Where(idea1 => idea1.IdeaId == id).Include(likes => likes.IdeasLikes).Include(u => u.User).SingleOrDefault();
            ViewBag.idea = idea;
            return View("Idea");
            }
            TempData["error_list"] = new List<string>() {"You need to login to get to this Page."};
            ViewBag.NoIdea = TempData["NoIdea"];
            return RedirectToAction("Index", "LoginRegistration");
        }

        [HttpGet]
        [Route("goBack")]
        public IActionResult GoBack()
        {
            return RedirectToAction("Index");
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