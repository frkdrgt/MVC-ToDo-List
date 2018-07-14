using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcToDoListApp.Models.VM
{
    public class ReminderVM
    {
        public Guid ID { get; set; }
        public int NotificationType { get; set; }
        public Guid UserID { get; set; }
        public Guid TaskID { get; set; }
        public string Date { get; set; }
        public int AutoID { get; set; }
    }
}