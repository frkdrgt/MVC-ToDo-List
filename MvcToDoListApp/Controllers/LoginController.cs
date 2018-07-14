using MvcToDoListApp.Models;
using MvcToDoListApp.Models.VM;
using MvcToDoListApp.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcToDoListApp.Controllers
{
    public class LoginController : Controller
    {
       
        TodoAppEntities db;
        public LoginController()
        {
            db = new TodoAppEntities();
        }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserVM u)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Where(x => x.Username.ToLower().Equals(u.Username.ToLower()) && x.Password.Equals(u.Password)).FirstOrDefault();
                if (user != null)
                {
                    Session["ID"] = user.ID;
                    Session["Username"] = user.Username;
                    Session["Mail"] = user.Mail;
                    Log.Info("[TODOAPP]: User logged in " + user.Username);

                    return RedirectToAction("Index", "Main");
                }
            }
            ModelState.AddModelError(nameof(u.Username), "Incorrect UserName");
            ModelState.AddModelError(nameof(u.Password), "Incorrect password");
            Log.Error("[TODOAPP]: User login error");
            return View();
        }
    }
}