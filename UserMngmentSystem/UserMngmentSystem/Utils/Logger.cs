using System;
using System.IO;

namespace UserMngmentSystem.Utils
{
    public class Logger
    {
        private readonly string _logFilePath;

        public Logger(string logFilePath = "application.log")
        {
            _logFilePath = logFilePath;
        }

        public void Log(string message)
        {
            File.AppendAllText(_logFilePath, message + Environment.NewLine);
        }
    }
}
