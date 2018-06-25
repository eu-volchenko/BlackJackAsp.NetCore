using System;
using System.IO;

namespace BlackJack.Utitlity.Utilities
{
    public static class LogWriter
    {
        public static void WriteLog(string message, string className)
        {
            try
            {
                var pathFile = AppDomain.CurrentDomain.BaseDirectory + "logFile.txt";
                if (!File.Exists(pathFile))
                {
                    using (var writer = File.CreateText(pathFile))
                    {
                        writer.WriteLine(DateTime.Now + " " + message + "; class: " + className + "</br>");
                        return;
                    }
                }

                using (var writer = File.AppendText(pathFile))
                {
                    writer.WriteLine(DateTime.Now + " " + message + "; class: " + className + "</br>");
                }

            }
            catch
            {
                WriteLog(message, className);
            }
        }
    }
}
