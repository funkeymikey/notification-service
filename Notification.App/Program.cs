using Notification.Core;
using System;

namespace Notification.App
{
    class Program
    {
        public Program()
        {
            Logger.Log("running console app");
            NotificationControlCenter notificationControlCenter = new NotificationControlCenter("Toast Notification Console App", "Toast.Notification.ConsoleApp");
            notificationControlCenter.StartConnection();
            Console.WriteLine("Listening for notifications, hit enter to exit");
            Console.Read();
            notificationControlCenter.StopConnection();
        }
        
        static void Main(string[] args)
        {
            new Program();
        }
    }
}
