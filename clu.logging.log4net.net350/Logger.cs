using clu.logging.library.net350;
using clu.logging.library.net350.Extensions;
using System;

namespace clu.logging.log4net.net350
{
    public class Logger
    {
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

        protected Logger()
        {
        }

        private enum LogLevel
        {
            Debug,
            Error,
            Fatal,
            Info,
            Warn
        }

        private class PostData
        {
            public string Message { get; set; }
        }

        private void Log(LogLevel level, string message)
        {
            var success = false;

            try
            {
                var apiUrl = $"http://localhost/clu.logging.webapi/Logging/{level}";

                var result = JsonClient.Post(new PostData { Message = message }, apiUrl);

                success = true;
            }
            catch (Exception ex) // [TODO] improve console logging
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Error occurred: {ex.ToExceptionMessage()}");
                Console.BackgroundColor = ConsoleColor.Black;
            }
            finally // [TODO] improve console logging
            {
                if (success)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"Logging succeeded for message: {message} ({level})!");
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"Logging failed for message: {message} ({level})!");
                    Console.BackgroundColor = ConsoleColor.Black;
                }
            }
        }

        public void LogDebug(string message)
        {
            Log(LogLevel.Debug, message);
        }

        public void LogError(string message)
        {
            Log(LogLevel.Error, message);
        }

        public void LogError(string message, Exception ex)
        {
            // [TODO] log exception when using logging api
        }

        public void LogFatal(string message)
        {
            Log(LogLevel.Fatal, message);
        }

        public void LogInformation(string message)
        {
            Log(LogLevel.Info, message);
        }

        public void LogWarning(string message)
        {
            Log(LogLevel.Warn, message);
        }
    }
}
