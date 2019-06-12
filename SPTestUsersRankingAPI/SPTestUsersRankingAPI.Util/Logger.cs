using System;
using System.IO;

namespace SPTestUsersRankingAPI.Util
{
    //I would use a framework like NLog or another, but for test purposes I created a simple "logger"
    public class Logger
    {
        protected readonly object LockObj = new object();
        public string FilePath = @"C:\Logs\";
        public string FileName = @"SPTestUsersRankingAPILog.txt";

        private static Logger instance;

        public static Logger Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Logger();
                }
                return instance;
            }
        }

        private Logger() {
            try
            {
                if (!Directory.Exists(FilePath))
                {
                    Directory.CreateDirectory(FilePath);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }

        private void WriteToFile(string type, string classFrom, string method, string message)
        {
            lock (LockObj)
            {
                using (StreamWriter streamWriter = File.AppendText(FilePath + FileName))
                {
                    streamWriter.WriteLine(string.Format("{0}|{1}|{2}|{3}|{4}",
                        DateTime.Now.ToString("MM/dd/yy H:mm:ss"),
                        classFrom,
                        method,
                        type,
                        message));

                    streamWriter.Close();
                }
            }
        }

        public void Trace(string classFrom, string method, string message)
        {
            WriteToFile("Trace", classFrom, method, message);
        }

        public void Info(string classFrom, string method, string message)
        {
            WriteToFile("Info", classFrom, method, message);
        }

        public void Debug(string classFrom, string method, string message)
        {
            WriteToFile("Debug", classFrom, method, message);
        }

        public void Error(string classFrom, string method, string message)
        {
            WriteToFile("Error", classFrom, method, message);
        }
    }

}
