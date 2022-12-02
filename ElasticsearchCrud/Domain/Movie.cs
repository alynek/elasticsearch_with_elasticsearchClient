namespace ElasticsearchCrud.Domain
{
    public class Movie
    {
        public string Id { get; set; }
        public List<string> Genre { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
    }
}