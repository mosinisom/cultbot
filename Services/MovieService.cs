using System.Text.Json;

public class Movie
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string CostumeDesigner { get; set; }
    public string Link { get; set; }
}

public class MovieService
{
    private Dictionary<string, List<Movie>> _movies;

    public MovieService()
    {
        var json = File.ReadAllText("movies.json");
        _movies = JsonSerializer.Deserialize<Dictionary<string, List<Movie>>>(json);
    }

    public List<Movie> GetMoviesByEra(string era)
    {
        if (_movies.ContainsKey(era))
        {
            return _movies[era];
        }
        return new List<Movie>();
    }
}