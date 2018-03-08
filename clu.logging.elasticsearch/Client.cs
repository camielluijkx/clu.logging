using Elasticsearch.Net;

using System;

namespace clu.logging.console
{
    // [TODO] handle errors: https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/elasticsearch-net-getting-started.html
    internal class Client
    {
        private static readonly Lazy<ElasticLowLevelClient> instance = new Lazy<ElasticLowLevelClient>(() => Initialize());

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
                .RequestTimeout(TimeSpan.FromMinutes(2));

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
