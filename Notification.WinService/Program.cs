﻿using System.ServiceProcess;

namespace Notification.WinService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new NotificationService() 
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
