namespace dadabase.Data
{
    public interface IJokeStore
    {
        Task<IEnumerable<Joke>> GetAllJokes();
        Task<Joke> GetJoke(int id, bool showDetails = false);
        Task<Joke> AddJoke(Joke Joke);
        Task<Joke> UpdateJoke(Joke Joke);
        Task DeleteJoke(int id);
        //Task<int> GetTimesTold(int id);
    }
}
