using System.Net.Http;
using System.Threading.Tasks;

namespace clu.logging.baconipsum.net461
{
    public static class BaconIpsumClient
    {
        public static async Task<string> GetAsync()
        {
            var data = "";

            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://baconipsum.com/api/?type=all-meat&paras=1&start-with-lorem=0&format=text");

            if (response.IsSuccessStatusCode)
            {
                data = await response.Content.ReadAsStringAsync();
            }

            return data;
        }
    }
}
