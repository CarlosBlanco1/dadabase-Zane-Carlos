namespace dadabase.Data
{
    public interface IJokereactioncategoryStore
    {
        Task<IEnumerable<Jokereactioncategory>> GetAllJokereactioncategorys();
        Task<Jokereactioncategory> GetJokereactioncategory(int id, bool showDetails = false);
        Task<Jokereactioncategory> AddJokereactioncategory(Jokereactioncategory Jokereactioncategory);
        Task<Jokereactioncategory> UpdateJokereactioncategory(Jokereactioncategory Jokereactioncategory);
        Task DeleteJokereactioncategory(int id);
    }
}
