using clu.logging.baconipsum.net350;
using clu.logging.library.net350.console;
using clu.logging.log4net.net350;

using System;
using System.Collections.Generic;

namespace clu.logging.console.net350
{
    class Program
    {
        private static void TestSomeLogging()
        {
            // [TODO] set correlation id when using logging api

            try
            {
                Logger.Instance.LogDebug("some debug message");
                Logger.Instance.LogError("some error message");
                Logger.Instance.LogFatal("some fatal message");
                Logger.Instance.LogInformation("some info message");
                Logger.Instance.LogWarning("some warn message");

                Logger.Instance.LogInformation("some secret password");

                //throw new Exception("some exception occurred");

                Logger.Instance.LogError("kaboom!", new ApplicationException("The application exploded!!"));
            }
            catch (Exception ex)
            {
                Logger.Instance.LogError("Error trying to do something:", ex);
            }
        }

        private static void TestRandomLogging(int i)
        {
            try
            {
                var ipsum = BaconIpsumClient.Get();

                var random = new Random();

                var dice = random.Next(1, 7);

                switch (dice)
                {
                    case 1:
                        {
                            Logger.Instance.LogDebug(ipsum);
                            break;
                        }
                    case 2:
                        {
                            Logger.Instance.LogError(ipsum);
                            break;
                        }
                    case 3:
                        {
                            Logger.Instance.LogFatal(ipsum);
                            break;
                        }
                    case 4:
                        {
                            Logger.Instance.LogInformation(ipsum);
                            break;
                        }
                    case 5:
                        {
                            Logger.Instance.LogWarning(ipsum);
                            break;
                        }
                    case 6:
                        {
                            Logger.Instance.LogError("bad luck", new Exception("no meat today"));
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.LogError("Error trying to do something", ex);
            }
        }

        private static void TestRandomLogging()
        {
            // [TODO] set correlation id when using logging api

            for (var i = 0; i < 100; i++)
            {
                TestRandomLogging(i);
            }
        }

        static void Main(string[] args)
        {
            ConsoleHelper.Initialize(".NET 3.5.0");
            ConsoleHelper.ShowMenu(
                new List<MenuItem>
                {
                    new MenuItem(4, "Test Some Logging", TestSomeLogging),
                    new MenuItem(5, "Test Random Logging", TestRandomLogging)
                });
        }
    }
}
