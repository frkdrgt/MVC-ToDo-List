using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcToDoListApp.Models.VM
{
    public class UserVM
    {
        public Guid ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public int AutoID { get; set; }
    }
}