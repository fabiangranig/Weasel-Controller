using System;
using System.IO;

namespace Weasel_Server_2.Logger
{
    internal class LoggerWorker
    {
        static string _LogLocation;

        static LoggerWorker()
        {
            _LogLocation = "log.txt";
        }

        public static void LogText(string loggedText)
        {
            string DateAndTime = DateTime.Now.ToString();
            File.AppendAllText(_LogLocation, DateAndTime + ": " + loggedText + "\n");
        }
    }
}
