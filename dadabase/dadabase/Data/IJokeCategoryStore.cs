namespace dadabase.Data
{
    public interface IJokeCategoryStore
    {
        Task<IEnumerable<Jokecategory>> GetAllJokecategorys();
        Task<Jokecategory> GetJokecategory(int id, bool showDetails = false);
        Task<Jokecategory> AddJokecategory(Jokecategory Jokecategory);
        Task<Jokecategory> UpdateJokecategory(Jokecategory Jokecategory);
        Task DeleteJokecategory(int id);
    }
}
