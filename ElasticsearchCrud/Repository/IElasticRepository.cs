using ElasticsearchCrud.Domain;
using System.Collections;

namespace ElasticsearchCrud.Repository
{
    public interface IElasticRepository
    {
        Task<Movie> GetMovieById(string id);
        Task<IEnumerable> GetAllMoviesByTitleField(string titleField);
        Task<IEnumerable> GetAllMovies();
        Task<bool> InsertMovie(Movie movie);
    }
}
