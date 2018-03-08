using Elasticsearch.Net;
using System;
using System.Threading.Tasks;

namespace clu.logging.console
{
    public static class Indexer
    {
        public static async Task<bool> BulkAsync(object[] data)
        {
            var body = PostData.MultiJson(data);
            var response = await Client.Instance.BulkAsync<StringResponse>(body);

#if DEBUG
            Console.WriteLine(response.DebugInformation);
#endif

            return response.Success;
        }

        public static async Task<bool> IndexAsync(string index, string type, string id, object data)
        {
            var body = PostData.Serializable(data);
            var response = await Client.Instance.IndexAsync<StringResponse>(index, type, id, body);

#if DEBUG
            Console.WriteLine(response.DebugInformation);
#endif

            return response.Success;
        }
    }
}
