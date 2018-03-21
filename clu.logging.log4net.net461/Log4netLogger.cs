using log4net;

using System;
using System.Reflection;
using System.Threading.Tasks;

namespace clu.logging.log4net.net461
{
    public class Log4netLogger
    {
        private readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static Log4netLogger instance;

        public static Log4netLogger Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Log4netLogger();
                }

                return instance;
            }
        }

        protected Log4netLogger()
        {
        }

        public async Task LogDebugAsync(string message)
        {
            await Task.Run(() =>
            {
                log.Debug(message);
            });
        }

        public async Task LogErrorAsync(string message)
        {
            await Task.Run(() =>
            {
                log.Error(message);
            });
        }

        public async Task LogErrorAsync(string message, Exception ex)
        {
            await Task.Run(() =>
            {
                log.Error(message, ex);
            });
        }

        public async Task LogFatalAsync(string message)
        {
            await Task.Run(() =>
            {
                log.Fatal(message);
            });
        }

        public async Task LogInformationAsync(string message)
        {
            await Task.Run(() =>
            {
                log.Info(message);
            });
        }

        public async Task LogWarningAsync(string message)
        {
            await Task.Run(() =>
            {
                log.Warn(message);
            });
        }
    }
}
