using clu.logging.library.net350;
using clu.logging.library.net350.Extensions;

using System;

namespace clu.logging.baconipsum.net350
{
    public static class BaconIpsumClient
    {
        public static string Get()
        {
            try
            {
                var apiUrl = "https://baconipsum.com/api?type=all-meat&paras=1&start-with-lorem=0&format=text";
                var ipsum = JsonClient.Get(apiUrl);

                if (ipsum == null)
                {
                    throw new InvalidOperationException("Unable to get bacon ipsum!");
                }

                return ipsum.ToString();
            }
            catch (Exception ex) // [TODO] improve console logging
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Error occurred: {ex.ToExceptionMessage()}");
                Console.BackgroundColor = ConsoleColor.Black;

                throw ex;
            }
        }
    }
}
