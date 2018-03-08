using Elasticsearch.Net;

using System;
using System.Threading.Tasks;

namespace clu.logging.console
{
    public static class Searcher
    {
        public static async Task<bool> SearchAsync(string index, string type, object data)
        {
            var body = PostData.Serializable(data);
            var response = await Client.Instance.SearchAsync<StringResponse>(index, type, body);

#if DEBUG
            Console.WriteLine(response.DebugInformation);
#endif

            return response.Success;
        }
    }
}
