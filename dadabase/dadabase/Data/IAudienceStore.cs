namespace dadabase.Data
{
    public interface IAudienceStore
    {
        Task<IEnumerable<Audience>> GetAllAudiences();
        Task<Audience> GetAudience(int id, bool showDetails = false);
        Task<Audience> AddAudience(Audience audience);
        Task<Audience> UpdateAudience(Audience audience);
        Task DeleteAudience(int id);       
    }
}
