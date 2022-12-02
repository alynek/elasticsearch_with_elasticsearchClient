using Elastic.Clients.Elasticsearch;
using ElasticsearchCrud.Domain;
using System.Collections;

namespace ElasticsearchCrud.Repository
{
    public class ElasticRepository : IElasticRepository
    {
        private readonly ElasticsearchClient _elasticClient;

        public ElasticRepository(ElasticsearchClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task<IEnumerable> GetAllMovies()
        {
            var result = await _elasticClient.SearchAsync<Movie>(m => m
                .Index("movies")
                .Size(1000));

            if (result.IsValidResponse) return result.Documents.AsEnumerable();

            return null;
        }

        public async Task<IEnumerable> GetAllMoviesByTitleField(string titleField)
        {
            var result = await _elasticClient.SearchAsync<Movie>(m => m
                .Index("movies")
                .From(0)
                .Size(10)
                .Query(q => q.MatchPhrasePrefix(p => p.Field(f => f.Title).Query(titleField)))
                );
            

            if (result.IsValidResponse) return result.Documents.AsEnumerable();

            return null;
        }
        
        
        public async Task<Movie> GetMovieById(string id)
        {
            var result = await _elasticClient.GetAsync<Movie>(id, idx => idx.Index("movies"));

            if (result.IsValidResponse) return result.Source;

            return null;
        }

    }
}
