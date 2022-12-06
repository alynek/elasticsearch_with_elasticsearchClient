using ElasticsearchCrud.Domain;
using Nest;
using System.Collections;

namespace ElasticsearchCrud.Repository
{
    public class ElasticRepository : IElasticRepository
    {
        private readonly IElasticClient _elasticClient;

        public ElasticRepository(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task<IEnumerable> GetAllMovies()
        {
            var result = await _elasticClient.SearchAsync<Movie>(m => m
                .Index("movies")
                .Size(1000));

            var resultWithIds = result.Hits.Select(h => {
                h.Source.Id = h.Id;
                return h.Source;
            });

            if (result.IsValid) return resultWithIds;

            return null;
        }

        public async Task<IEnumerable> GetAllMoviesByTitleField(string titleField)
        {
            var result = await _elasticClient.SearchAsync<Movie>(m => m
                .Index("movies")
                .From(0)
                .Size(10)
                .Query(q => q.MatchPhrasePrefix(p => p.Field(f => f.Title)
                    .Query(titleField)))
                );

            var resultWithIds = result.Hits.Select(h =>
            {
                h.Source.Id = h.Id;
                return h.Source;
            });


            if (result.IsValid) return resultWithIds;

            return null;
        }


        public async Task<Movie> GetMovieById(string id)
        {
            var result = await _elasticClient.GetAsync<Movie>(id, idx => idx.Index("movies"));


            if (result.IsValid)
            {
                result.Source.Id = result.Id;
                return result.Source;
            }

            return null;
        }

        public async Task<bool> InsertMovie(Movie movie)
        {
            var result = await _elasticClient.IndexAsync(movie, k => k.Index("movies"));
            return result.IsValid;
        }
    }
}
