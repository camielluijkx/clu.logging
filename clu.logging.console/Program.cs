using clu.logging.baconipsum;
using clu.logging.log4net;

using log4net;

using System;
using System.Threading.Tasks;

namespace clu.logging.console
{
    // [TODO] automate cleanup of log data
    // [TODO] implement clu.logging.webapi (+ client) for clu.console.demo.net35
    // [TODO] explore NLog.Targets.ElasticSearch in a separate clu.logging.nlog library | nuget package
    // [TODO] setup pipeline for automated build and publish of nuget package (based on commit to master)
    class Program
    {
        private static void Initialize()
        {
            Console.WriteLine("Initializing...");

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("****************************************************************");
            Console.WriteLine("*******************      Logging Console     *******************");
            Console.WriteLine("****************************************************************");
            Console.WriteLine("");
            Console.WriteLine("");

            Console.BackgroundColor = ConsoleColor.Blue;

            Console.WriteLine("\t\t\t\t\t\t\t\t");
            Console.WriteLine(@"                 _________ .____     ____ ___                   ");
            Console.WriteLine(@"                 \_   ___ \|    |   |    |   \                  ");
            Console.WriteLine(@"                 /    \  \/|    |   |    |   /                  ");
            Console.WriteLine(@"                 \     \___|    |___|    |  /                   ");
            Console.WriteLine(@"                  \______  /_______ \______/                    ");
            Console.WriteLine(@"                         \/        \/                           ");
            Console.WriteLine("\t\t\t\t\t\t\t\t");
            Console.WriteLine("");
            Console.WriteLine("");

            Console.BackgroundColor = ConsoleColor.Black;
        }

        private static async Task TestBulkAsync()
        {
            var data = new object[]
            {
                new { index = new { _index = "people", _type = "person", _id = "2"  }},
                new { FirstName = "Joyce", LastName = "Luijkx" },
                new { index = new { _index = "people", _type = "person", _id = "3"  }},
                new { FirstName = "Jimmy", LastName = "Luijkx" },
                new { index = new { _index = "people", _type = "person", _id = "4"  }},
                new { FirstName = "Dean", LastName = "Luijkx" },
            };

            var response = await ElasticSearchService.BulkAsync(data);
        }

        private static void TestBulk()
        {
            TestBulkAsync().Wait();
        }

        private static async Task TestIndexAsync()
        {
            var data = new
            {
                FirstName = "Camiel",
                LastName = "Luijkx"
            };

            var response = await ElasticSearchService.IndexAsync("people", "person", "1", data);
        }

        private static void TestIndex()
        {
            TestIndexAsync().Wait();
        }

        private static async Task TestSearchAsync()
        {
            var data = new
            {
                query = new
                {
                    query_string = new
                    {
                        query = "Luijkx"
                    }
                }
            };

            //var data = @"
            //{
            //  ""query"": {
            //    ""query_string"": {
            //      ""query"": ""Luijkx""
            //    }
            //  }
            //}";

            var response = await ElasticSearchService.SearchAsync("people", "person", data);
        }

        private static void TestSearch()
        {
            TestSearchAsync().Wait();
        }

        private async static Task TestSomeLoggingAsync()
        {
            GlobalContext.Properties["correlationId"] = Guid.NewGuid();

            try
            {
                await Log4netLogger.Instance.LogDebugAsync("some debug message");
                await Log4netLogger.Instance.LogErrorAsync("some error message");
                await Log4netLogger.Instance.LogFatalAsync("some fatal message");
                await Log4netLogger.Instance.LogInformationAsync("some info message");
                await Log4netLogger.Instance.LogWarningAsync("some warning message");

                await Log4netLogger.Instance.LogInformationAsync("some stupid password");

                //throw new Exception("some exception occurred");

                await Log4netLogger.Instance.LogErrorAsync("kaboom!", new ApplicationException("The application exploded"));
            }
            catch (Exception ex)
            {
                await Log4netLogger.Instance.LogErrorAsync("Error trying to do something", ex);
            }
        }

        private static void TestSomeLogging()
        {
            TestSomeLoggingAsync().Wait();
        }

        private async static Task TestRandomLoggingAsync()
        {
            GlobalContext.Properties["correlationId"] = Guid.NewGuid();

            try
            {
                var ipsum = await BaconIpsumClient.GetAsync();

                var random = new Random();

                var dice = random.Next(1, 7);

                switch (dice)
                {
                    case 1:
                        await Log4netLogger.Instance.LogDebugAsync(ipsum);
                        break;
                    case 2:
                        await Log4netLogger.Instance.LogErrorAsync(ipsum);
                        break;
                    case 3:
                        await Log4netLogger.Instance.LogFatalAsync(ipsum);
                        break;
                    case 4:
                        await Log4netLogger.Instance.LogInformationAsync(ipsum);
                        break;
                    case 5:
                        await Log4netLogger.Instance.LogWarningAsync(ipsum);
                        break;
                    case 6:
                        await Log4netLogger.Instance.LogErrorAsync("bad luck", new Exception("no meat today"));
                        break;
                }
            }
            catch (Exception ex)
            {
                await Log4netLogger.Instance.LogErrorAsync("Error trying to do something", ex);
            }
        }

        private static void TestRandomLogging()
        {
            for (var i = 0; i < 100; i++)
            {
                TestRandomLoggingAsync().Wait();
            }
        }

        private static void ShowMenu()
        {
            Console.WriteLine("Hello {0}, what would you like to do?", Environment.UserName);

            char choice = ' ';
            while (choice != '0')
            {
                Console.WriteLine("");
                Console.WriteLine("[0] Exit");
                Console.WriteLine("[1] Bulk");
                Console.WriteLine("[2] Index");
                Console.WriteLine("[3] Search");
                Console.WriteLine("[4] Logging");
                ConsoleKeyInfo consoleKey = Console.ReadKey(true);
                choice = consoleKey.KeyChar;

                if (choice == '1')
                {
                    TestBulk();
                }
                else if (choice == '2')
                {
                    TestIndex();
                }
                else if (choice == '3')
                {
                    TestSearch();
                }
                else if (choice == '4')
                {
                    //TestSomeLogging();
                    TestRandomLogging();
                }
            }

            Environment.Exit(0);
        }

        static void Main(string[] args)
        {
            Initialize();
            ShowMenu();
        }
    }
}
