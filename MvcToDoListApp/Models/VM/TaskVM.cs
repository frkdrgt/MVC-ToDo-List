using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcToDoListApp.Models.VM
{
    public class TaskVM
    {
        public Guid ID { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid DashboardID { get; set; }
        public int AutoID { get; set; }
    }
}