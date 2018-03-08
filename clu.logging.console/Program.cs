using clu.logging.log4net;

using log4net;

using System;
using System.Threading.Tasks;

namespace clu.logging.console
{
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

            var response = await Indexer.BulkAsync(data);
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

            var response = await Indexer.IndexAsync("people", "person", "1", data);
        }

        private static void TestIndex()
        {
            TestIndexAsync().Wait();
        }

        private static async Task TestSearchAsync() // [TODO] fix search query: https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/elasticsearch-net-getting-started.html
        {
            var data = new
            {
                from = 0,
                size = 10,
                query = new
                {
                    match = new
                    {
                        field = "firstName",
                        query = "Jimmy"
                    }
                }
            };

            var response = await Searcher.SearchAsync("people", "person", data);
        }

        private static void TestSearch()
        {
            TestSearchAsync().Wait();
        }

        private async static Task TestLoggingAsync()
        {
            GlobalContext.Properties["correlationId"] = Guid.NewGuid();

            try
            {
                await Logger.LogDebugAsync("some debug message");
                await Logger.LogErrorAsync("some error message");
                await Logger.LogFatalAsync("some fatal message");
                await Logger.LogInformationAsync("some info message");
                await Logger.LogWarningAsync("some warning message");

                await Logger.LogInformationAsync("some stupid password");

                //throw new Exception("some exception occurred");

                await Logger.LogErrorAsync("kaboom!", new ApplicationException("The application exploded"));
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync("Error trying to do something", ex);
            }
        }

        private static void TestLogging()
        {
            TestLoggingAsync().Wait();
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
                    TestLogging();
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
