using Elasticsearch.Net;

using System;
using System.Threading.Tasks;

namespace clu.logging.console
{
    internal class Client
    {
        private static readonly Lazy<ElasticLowLevelClient> instance = new Lazy<ElasticLowLevelClient>(() => Initialize());

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

        private Client()
        {
            Initialize();
        }

        internal static ElasticLowLevelClient Instance
        {
            get
            {
                return instance.Value;
            }
        }
    }
}
