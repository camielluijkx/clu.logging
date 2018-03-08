using log4net;

using System;
using System.Reflection;
using System.Threading.Tasks;

namespace clu.logging.log4net
{
    // [TODO] debug post build > create nuget package with bat script
    // [TODO] release post build > create + publish nuget package with bat script
    // [TODO] demo application to install nuget package
    // [TODO] pipeline for automated build and publish of nuget package
    public static class Log4netLogger
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public async static Task LogDebugAsync(string message)
        {
            await Task.Run(() =>
            {
                log.Debug(message);
            });
        }

        public async static Task LogErrorAsync(string message)
        {
            await Task.Run(() =>
            {
                log.Error(message);
            });
        }

        public async static Task LogErrorAsync(string message, Exception ex)
        {
            await Task.Run(() =>
            {
                log.Error(message, ex);
            });
        }

        public async static Task LogFatalAsync(string message)
        {
            await Task.Run(() =>
            {
                log.Fatal(message);
            });
        }

        public async static Task LogInformationAsync(string message)
        {
            await Task.Run(() =>
            {
                log.Info(message);
            });
        }

        public async static Task LogWarningAsync(string message)
        {
            await Task.Run(() =>
            {
                log.Warn(message);
            });
        }
    }
}
