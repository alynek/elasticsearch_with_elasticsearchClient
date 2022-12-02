using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.Extensions.Configuration;

namespace ElasticsearchCrud
{
    public static class ElasticConfiguration
    {
        public static void AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
        {
            ElasticsearchClientSettings settings = 
                new ElasticsearchClientSettings(new Uri(configuration["ElasticsearchSettings:uri"]))
            .Authentication(new BasicAuthentication(configuration["ElasticsearchSettings:login"], configuration["ElasticsearchSettings:password"]));

            var client = new ElasticsearchClient(settings);

            services.AddSingleton(client);
        }
    }
}
