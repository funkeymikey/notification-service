using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Core
{
    public class Logger
    {
        public static void Log(String message)
        {
            //DateTime now = DateTime.Now;
            //String logFile = "output." + now.Year + "-" + now.Month + "-" + now.Day + ".log";

            //File.AppendAllText(logFile, message + "\n");

            Console.WriteLine(message);
        }
    }
}
