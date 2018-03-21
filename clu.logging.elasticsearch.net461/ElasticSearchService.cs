using Elasticsearch.Net;

using System;
using System.Threading.Tasks;

namespace clu.logging.console.net461
{
    public static class ElasticSearchService
    {
        private static readonly Lazy<ElasticLowLevelClient> elasticSearchClient = new Lazy<ElasticLowLevelClient>(() => Initialize());

        private static Task HandleResponseAsync(StringResponse response)
        {
#if DEBUG
            var success = response.Success;
            var successOrKnownError = response.SuccessOrKnownError;
            var exception = response.OriginalException;

            Console.WriteLine(response.DebugInformation);
#endif

            return Task.FromResult(0);
        }

        private static ElasticLowLevelClient Initialize()
        {
            var uris = new[]
            {
                new Uri("http://localhost:9200"),
                //new Uri("http://localhost:9201"),
                //new Uri("http://localhost:9202"),
            };

            var connectionPool = new SniffingConnectionPool(uris);
            var settings = new ConnectionConfiguration(connectionPool)
                .RequestTimeout(TimeSpan.FromMinutes(2))
                .ThrowExceptions()
                .OnRequestCompleted(apiCallDetails =>
                {
                    if (apiCallDetails.HttpStatusCode == 400)
                    {
                        throw new ElasticsearchClientException("you probably provided a malformed request");
                    }
                });

             return new ElasticLowLevelClient(settings);
        }

        public async static Task<bool> BulkAsync(object[] data)
        {
            var body = PostData.MultiJson(data);
            var response = await elasticSearchClient.Value.BulkAsync<StringResponse>(body);

            await HandleResponseAsync(response);

            return response.Success;
        }

        public async static Task<bool> IndexAsync(string index, string type, string id, object data)
        {
            var body = PostData.Serializable(data);
            var response = await elasticSearchClient.Value.IndexAsync<StringResponse>(index, type, id, body);

            await HandleResponseAsync(response);

            return response.Success;
        }

        public async static Task<bool> SearchAsync(string index, string type, object data)
        {
            var body = PostData.Serializable(data);
            var response = await elasticSearchClient.Value.SearchAsync<StringResponse>(index, type, body);

            await HandleResponseAsync(response);

            return response.Success;
        }

        public async static Task<bool> SearchAsync(string index, string type, string data)
        {
            var response = await elasticSearchClient.Value.SearchAsync<StringResponse>(index, type, data);

            await HandleResponseAsync(response);

            return response.Success;
        }
    }
}
