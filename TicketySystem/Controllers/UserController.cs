using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using TicketySystem.DAL;
using TicketySystem.Models;
using static TicketySystem.Models.User;

namespace TicketySystem.Controllers
{
    public class UserController : Controller
    {
        User_DAL _UserDAL = new User_DAL();
     

        // GET: User
        public ActionResult Index()
        {
            var UserList = _UserDAL.GetAllUser();
            return View(UserList);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        [CustomAuthorize("Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [CustomAuthorize("Administrator")]
        public ActionResult Create(User user)
        {
            try
            {
                bool IsInserted = false;
                if(ModelState.IsValid)
                {
                  IsInserted = _UserDAL.insertUser(user);
                    if(IsInserted)
                    {
                        TempData["SuccessMessage"] = "New User saved successfully...!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to saved User successfully...!";
                    }
                    
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
