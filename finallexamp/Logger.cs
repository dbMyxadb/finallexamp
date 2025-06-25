using System;
using System.IO;

namespace finallexamp.Logging
{
    public static class Logger
    {
        private static readonly string logFile = "log.txt";

        public static void Log(string message)
        {
            var logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";
            File.AppendAllText(logFile, logMessage + Environment.NewLine);
        }
    }
}
