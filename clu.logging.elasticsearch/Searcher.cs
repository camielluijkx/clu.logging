using Elasticsearch.Net;

using System;
using System.Threading.Tasks;

namespace clu.logging.console
{
    /// <summary>
    /// Searcher class depends on ElasticSearch version for DSL syntax.
    /// </summary>
    public static class Searcher
    {
        public static async Task<bool> SearchAsync(string index, string type, object data)
        {
            var body = PostData.Serializable(data);
            var response = await Client.Instance.SearchAsync<StringResponse>(index, type, body);

#if DEBUG
            var success = response.Success;
            var successOrKnownError = response.SuccessOrKnownError;
            var exception = response.OriginalException;

            Console.WriteLine(response.DebugInformation);
#endif

            return response.Success;
        }

        public static async Task<bool> SearchAsync(string index, string type, string data)
        {
            var response = await Client.Instance.SearchAsync<StringResponse>(index, type, data);

#if DEBUG
            var success = response.Success;
            var successOrKnownError = response.SuccessOrKnownError;
            var exception = response.OriginalException;

            Console.WriteLine(response.DebugInformation);
#endif

            return response.Success;
        }
    }
}
