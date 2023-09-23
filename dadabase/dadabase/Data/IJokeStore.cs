namespace dadabase.Data
{
    public interface IJokeStore
    {
        Task<IEnumerable<Joke>> GetAllJokes();
        Task<Joke> GetJoke(int id, bool showDetails = false);
        Task<Joke> AddJoke(Joke Joke);
        Task<Joke> UpdateJoke(Joke Joke);
        Task DeleteJoke(int id);
        Task<IEnumerable<Joke>> GetJokesByCategory(string category);
        Task<IEnumerable<Joke>> GetJokesByAudience(string inputAudience);
        Task<IEnumerable<Joke>> GetJokesByReaction();
        Task<IEnumerable<Joke>> GetJokesRankedGivenCategory(string inputCategory);
        Task<IEnumerable<Joke>> GetJokesRankedGivenAudience(string inputAudience);
        Task<int> GetNumberOfTimesTold(string jokeName);
    }
}
