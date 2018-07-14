using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcToDoListApp.Utility
{
    public class JsonResponse
    {
        public bool Success { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
         
    }

    
}