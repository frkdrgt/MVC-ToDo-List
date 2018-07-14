using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcToDoListApp.Models.VM
{
    public class DashboardVM
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public int AutoID { get; set; }
    }
}