namespace dadabase.Data
{
    public interface IDataStore
    {
        Task<IEnumerable<Joke>> GetAllJokes();
        Task<Joke> GetJoke(int id, bool showDetails = false);
        Task<Joke> AddJoke(Joke Joke);
        Task<Joke> UpdateJoke(Joke Joke);
        Task DeleteJoke(int id);

        Task<IEnumerable<Audience>> GetAllAudiences();
        Task<Audience> GetAudience(int id, bool showDetails = false);
        Task<Audience> AddAudience(Audience audience);
        Task<Audience> UpdateAudience(Audience audience);
        Task DeleteAudience(int id);
    }
}
