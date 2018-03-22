using clu.logging.baconipsum.net461;
using clu.logging.library.net350.console;
using clu.logging.log4net.net461;

using log4net;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace clu.logging.console.net461
{
    // [TODO] clu.console (library) as nuget package
    // [TODO] explore NLog.Targets.ElasticSearch in a separate clu.logging.nlog library | nuget package
    // [TODO] setup pipeline for automated build and publish of nuget package (based on commit to master)
    class Program
    {
        private static async Task TestBulkIndexAsync()
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

        private static void TestBulkIndex()
        {
            TestBulkIndexAsync().Wait();
        }

        private static async Task TestSingleIndexAsync()
        {
            var data = new
            {
                FirstName = "Camiel",
                LastName = "Luijkx"
            };

            var response = await ElasticSearchService.IndexAsync("people", "person", "1", data);
        }

        private static void TestSingleIndex()
        {
            TestSingleIndexAsync().Wait();
        }

        private static async Task TestSearchIndexAsync()
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

        private static void TestSearchIndex()
        {
            TestSearchIndexAsync().Wait();
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
                await Log4netLogger.Instance.LogWarningAsync("some warn message");

                await Log4netLogger.Instance.LogInformationAsync("some secret password");

                //throw new Exception("some exception occurred");

                await Log4netLogger.Instance.LogErrorAsync("kaboom!", new ApplicationException("The application exploded!!"));
            }
            catch (Exception ex)
            {
                await Log4netLogger.Instance.LogErrorAsync("Error trying to do something:", ex);
            }
        }

        private static void TestSomeLogging()
        {
            TestSomeLoggingAsync().Wait();
        }

        private async static Task TestRandomLoggingAsync()
        {
            try
            {
                var ipsum = await BaconIpsumClient.GetAsync();

                var random = new Random();

                var dice = random.Next(1, 7);

                switch (dice)
                {
                    case 1:
                        {
                            await Log4netLogger.Instance.LogDebugAsync(ipsum);
                            break;
                        }
                    case 2:
                        {
                            await Log4netLogger.Instance.LogErrorAsync(ipsum);
                            break;
                        }
                    case 3:
                        {
                            await Log4netLogger.Instance.LogFatalAsync(ipsum);
                            break;
                        }
                    case 4:
                        {
                            await Log4netLogger.Instance.LogInformationAsync(ipsum);
                            break;
                        }
                    case 5:
                        {
                            await Log4netLogger.Instance.LogWarningAsync(ipsum);
                            break;
                        }
                    case 6:
                        {
                            await Log4netLogger.Instance.LogErrorAsync("bad luck", new Exception("no meat today"));
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                await Log4netLogger.Instance.LogErrorAsync("Error trying to do something", ex);
            }
        }

        private static void TestRandomLogging(int i)
        {
            TestRandomLoggingAsync().Wait();
        }

        private static void TestRandomLogging()
        {
            GlobalContext.Properties["correlationId"] = Guid.NewGuid();

            for (var i = 0; i < 100; i++)
            {
                TestRandomLogging(i);
            }
        }

        static void Main(string[] args)
        {
            ConsoleHelper.Initialize(".NET 4.6.1");
            ConsoleHelper.ShowMenu(
                new List<MenuItem>
                {
                    new MenuItem(1, "Test Bulk Index", TestBulkIndex),
                    new MenuItem(2, "Test Single Index", TestSingleIndex),
                    new MenuItem(3, "Test Search Index", TestSearchIndex),
                    new MenuItem(4, "Test Some Logging", TestSomeLogging),
                    new MenuItem(5, "Test Random Logging", TestRandomLogging)
                });
        }
    }
}
