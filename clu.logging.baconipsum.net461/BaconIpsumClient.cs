using clu.logging.library.extensions.net461;

using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace clu.logging.baconipsum.net461
{
    public static class BaconIpsumClient
    {
        public static async Task<string> GetAsync()
        {
            try
            {
                var data = "";

                var client = new HttpClient();
                var apiUri = new Uri("https://baconipsum.com/api/?type=all-meat&paras=1&start-with-lorem=0&format=text");

                HttpResponseMessage response = await client.GetAsync(apiUri);

                if (response.IsSuccessStatusCode)
                {
                    data = await response.Content.ReadAsStringAsync();
                }

                return data;
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
