using Nest;

namespace ElasticsearchCrud
{
    public static class ElasticConfiguration
    {
        public static void AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = new ConnectionSettings(new Uri(configuration["ElasticsearchSettings:uri"]))
            .BasicAuthentication(configuration["ElasticsearchSettings:login"], configuration["ElasticsearchSettings:password"]);

            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);
        }
    }
}
