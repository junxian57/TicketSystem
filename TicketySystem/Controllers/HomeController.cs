using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static TicketySystem.Models.User;
using TicketySystem.DAL;
using TicketySystem.Models;

namespace TicketySystem.Controllers
{
    public class HomeController : Controller
    {

        User_DAL _UserDAL = new User_DAL();
        public ActionResult Index()
        {
            if (Session["userRole"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            List<User> user = _UserDAL.login(username, password);


            if (user != null && user.Count > 0)
            {
                Session["userRole"] = user[0].UserRole;
                Session["userGuid"] = user[0].Id;
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View();
            }

        }
    }
}