using Hangfire;
using Microsoft.Owin;
using MvcToDoListApp.Service;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(MvcToDoListApp.Startup))]
namespace MvcToDoListApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            GlobalConfiguration.Configuration.UseSqlServerStorage(@"Server=FRKDRGT;Database=TodoApp;Integrated Security=true");
            app.UseHangfireDashboard();
            app.UseHangfireServer();
            ReminderService service = new ReminderService();


            RecurringJob.AddOrUpdate(() => service.NotifySend(), Cron.Minutely());
        }
    }
}