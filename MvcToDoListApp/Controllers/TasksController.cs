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
    public class TasksController : Controller
    {
        TodoAppEntities db;
        public TasksController()
        {
            db = new TodoAppEntities();
        }

        public ActionResult Index(Guid? id)
        {
          
            if (Session["ID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return RedirectToAction("Index", "Main");
            }
            TaskVM taskVM = new TaskVM();
            taskVM.DashboardID = id.Value;
            return View(taskVM);
        }

        [HttpPost]
        public JsonResult Add(TaskVM task)
        {
            if (!String.IsNullOrWhiteSpace(task.Description))
            {
                Task t = new Task();
                t.ID = Guid.NewGuid();
                t.Description = task.Description;
                t.DashboardID = task.DashboardID;
                t.CreatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                db.Tasks.Add(t);

                var result = db.SaveChanges();
                if (result > 0)
                {
                    Log.Debug("[TODOAPP]: Task creatad " + t.Description);
                    return JsonSuccess(t, "Task created!");
                }
                else
                {
                    Log.Error("[TODOAPP]: Task can't creatad");
                    return JsonError("Task can't added!");
                }
            }
            else
            {
                Log.Error("[TODOAPP]: Task description empty error");
                return JsonError("Task can't added!");
            }
        }
        [HttpPost]
        public ActionResult Update(TaskVM task)
        {
            Task updatedTask = db.Tasks.Find(task.ID);
            if (updatedTask != null)
            {
                updatedTask.Description = task.Description;
                db.SaveChanges();

                Log.Debug("[TODOAPP]: Task updated ");
                return JsonSuccess(null, "Task Updated");
            }
            else
            {
                Log.Error("[TODOAPP]: Task updated failed");
                return JsonError("Task Update failed");
            }
            
        }
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            Task deletedTask = db.Tasks.Find(id);
            if (deletedTask != null)
            {
                db.Tasks.Remove(deletedTask);
                db.SaveChanges();

                Log.Debug("[TODOAPP]: Task deleted ");
                return JsonSuccess("Task Deleted");
            }
            else
            {
                Log.Error("[TODOAPP]: Task delete failed");
                return JsonError("Task Delete failed");
            }
        }

        [HttpPost]
        public JsonResult List(Guid id)
        {
            var allTask = db.Tasks.Where(x => x.DashboardID == id).ToList();
            return JsonSuccess(allTask);
        }

        [HttpPost]
        public JsonResult AddReminder(ReminderVM reminder)
        {
            if (!String.IsNullOrWhiteSpace(reminder.Date.ToString()) && reminder.NotificationType != 0)
            {
                Reminder r = new Reminder();
                r.ID = Guid.NewGuid();
                r.TaskID = reminder.TaskID;
                r.NotificationType = reminder.NotificationType;
                r.UserID = Guid.Parse(Session["ID"].ToString());
                r.Date = reminder.Date;
                r.IsSend = false;
                db.Reminders.Add(r);

                var result = db.SaveChanges();
                if (result > 0)
                {
                    Log.Debug("[TODOAPP]: Reminder created  "+ r.Date);
                    return JsonSuccess(null, "Reminder created!");
                }
                else
                {
                    Log.Error("[TODOAPP]: Reminder can't added");
                    return JsonError("Reminder can't added!");
                }
            }
            else
            {
                Log.Error("[TODOAPP]: Reminder can't added");
                return JsonError("Reminder can't added!");
            }
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