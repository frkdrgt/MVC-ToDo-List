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
    public class MainController : Controller
    {
        TodoAppEntities db;
        public MainController()
        {
            db = new TodoAppEntities();
        }
        
        public ActionResult Index()
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
        
        [HttpPost]
        public JsonResult Add(DashboardVM dashboard)
        {
            if (!String.IsNullOrWhiteSpace(dashboard.Name))
            {
                Dashboard d = new Dashboard();
                d.ID = Guid.NewGuid();
                d.Name = dashboard.Name;
                d.CreatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());

                db.Dashboards.Add(d);
                var result = db.SaveChanges();
                if (result > 0)
                {
                    Log.Debug("[TODOAPP]: New dashboard created" + d.Name);
                    return JsonSuccess(d, "Dashboard created!");
                    
                }
                else
                {
                    Log.Error("[TODOAPP]: Dashboard error(check db)");
                    return JsonError("Dashboard can't added!");
                }
            }
            else
            {
                Log.Error("[TODOAPP]: Dashboard name empty error");

                return JsonError("Dashboard can't added!");
            }
        }

        [HttpPost]
        public ActionResult Update(DashboardVM dashboard)
        {
            Dashboard updatedDashboard = db.Dashboards.Find(dashboard.ID);
            if (updatedDashboard != null)
            {
                updatedDashboard.Name = dashboard.Name;
                db.SaveChanges();
                Log.Debug("[TODOAPP]: Dashboard updated " + updatedDashboard.Name);
                return JsonSuccess(null, "Updated");
            }
            else
            {
                Log.Error("[TODOAPP]: Dashboard update failed");
                return JsonError("Update fail");

               
            }
           
        }
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            Dashboard deletedDashboard = db.Dashboards.Find(id);
            if(deletedDashboard != null)
            {
                db.Dashboards.Remove(deletedDashboard);
                db.SaveChanges();

                Log.Debug("[TODOAPP]: Dashboard deleted ");
                return JsonSuccess(null, "Deleted");
            }
            else
            {
                Log.Error("[TODOAPP]: Dashboard delete failed");
                return JsonError("Delete failed");
            }
           
        }


        public JsonResult List()
        {
            var allDashboard =db.Dashboards.ToList();
            return JsonSuccess(allDashboard);
        }
         
        public JsonResult JsonSuccess(object data, string message = "")
        {
            return Json(new JsonResponse
            {

                Data = data,
                Message = message,
                Success = true
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult JsonError(string message)
        {
            return Json(new JsonResponse
            {
                Data = null,
                Success = false,
                Message = message
            }, JsonRequestBehavior.AllowGet);
        }

    }
}