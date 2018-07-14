using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcToDoListApp.Utility
{
    public class Log
    {
        private static readonly Log _instance = new Log();
        protected ILog monitoringLogger;
        protected static ILog debugLogger;

        private Log()
        {
            monitoringLogger = LogManager.GetLogger("MonitoringLogger");
            debugLogger = LogManager.GetLogger("DebugLogger");
        }
         
        public static void Debug(string message)
        {
            debugLogger.Debug(message);
        }

 
        public static void Debug(string message, System.Exception exception)
        {
            debugLogger.Debug(message, exception);
        }

 
        public static void Info(string message)
        {
            _instance.monitoringLogger.Info(message);
        }


       
        public static void Info(string message, System.Exception exception)
        {
            _instance.monitoringLogger.Info(message, exception);
        }

      
        public static void Warn(string message)
        {
            _instance.monitoringLogger.Warn(message);
        }

       
        public static void Warn(string message, System.Exception exception)
        {
            _instance.monitoringLogger.Warn(message, exception);
        }

        
        public static void Error(string message)
        {
            _instance.monitoringLogger.Error(message);
        }

       
        public static void Error(string message, System.Exception exception)
        {
            _instance.monitoringLogger.Error(message, exception);
        }


      
        public static void Fatal(string message)
        {
            _instance.monitoringLogger.Fatal(message);
        }

        
        public static void Fatal(string message, System.Exception exception)
        {
            _instance.monitoringLogger.Fatal(message, exception);
        }
    }
}