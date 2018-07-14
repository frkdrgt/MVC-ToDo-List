using MvcToDoListApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace MvcToDoListApp.Service
{
    public class ReminderService
    {

        public void NotifySend()
        {
            TodoAppEntities db = new TodoAppEntities();

            var reminders = db.Reminders.ToList();
            if (reminders.Count > 0)
            {
                foreach (var item in reminders)
                {
                    if (item.IsSend==false)
                    {
                        var date = DateTime.Parse(item.Date, new CultureInfo("en-US", true));

                        if (date.Date == DateTime.Today)
                        {
                            var user = db.Users.Where(x => x.ID == item.UserID).FirstOrDefault();
                            var task = db.Tasks.Where(x => x.ID == item.TaskID).FirstOrDefault();
                            string taskDesc = "";
                            if (task != null)
                            {
                                taskDesc = task.Description;
                            }
                            else
                            {
                                taskDesc = "";
                            }
                            if (user != null)
                            {
                                // NotificationType = 1 Mail
                                if (item.NotificationType == 1)
                                {
                                    try
                                    {
                                        SmtpClient sc = new SmtpClient();
                                        sc.Port = 587;
                                        sc.Host = "smtp.gmail.com";
                                        sc.EnableSsl = true;
                                        sc.Credentials = new NetworkCredential("codexscreen@gmail.com", "***");
                                        MailMessage mail = new MailMessage();
                                        mail.From = new MailAddress("codexscreen@gmail.com", "Task Reminder");
                                        mail.To.Add(user.Mail);
                                        mail.Subject = "Task Reminder";
                                        mail.IsBodyHtml = true;
                                        mail.Body = string.Format("Do not forget this task! Just do it. <br/> {0}", taskDesc);
                                        sc.Send(mail);
                                        item.IsSend = true;
                                        db.SaveChanges();
                                    }
                                    catch (Exception e)
                                    {

                                    }
                                }
                                else if (item.NotificationType == 2)
                                {
                                    //SMS LOGIC 
                                }
                            }
                        }
                    }
                }
            }
        }
       
    }
}
