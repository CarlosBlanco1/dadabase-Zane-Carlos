using Microsoft.EntityFrameworkCore;

namespace dadabase.Data
{
    public class PostgresJokeCategoryDataStore: IJokeCategoryStore
    {
        private readonly JokeContext context;
        private readonly ILogger<PostgresJokeDataStore> logger;

        public PostgresJokeCategoryDataStore(JokeContext context, ILogger<PostgresJokeDataStore> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<Jokecategory> AddJokecategory(Jokecategory Jokecategory)
        {
            context.Jokecategories.Add(Jokecategory);
            await context.SaveChangesAsync();
            return Jokecategory;
        }

        public async Task DeleteJokecategory(int id)
        {
            var existingRecipe = await context.Jokecategories.FindAsync(id);
            if (existingRecipe is null)
            {
                throw new ArgumentException($"Recipe with id {id} does not exist");
            }
            context.Jokecategories.Remove(existingRecipe);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Jokecategory>> GetAllJokecategorys() => await context.Jokecategories.ToListAsync();

        public async Task<Jokecategory> GetJokecategory(int id, bool showDetails = false)
        {
            if (showDetails)
            {
                return await context.Jokecategories
                    .Include(c => c.Categorizedjokes)
                    .ThenInclude(c => c.Jokecategory)
                    .FirstOrDefaultAsync(c => c.Id == id);
            }
            return await context.Jokecategories.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Jokecategory> UpdateJokecategory(Jokecategory Jokecategory)
        {
            await Task.CompletedTask;
            var value = await context.Jokecategories.Include(c => c.Categorizedjokes)
                    .ThenInclude(c => c.Jokecategory)
            .FirstOrDefaultAsync(r => r.Id == Jokecategory.Id);
            value.Categoryname = Jokecategory. Categoryname;
            //ask about changing the category and delivery info as well
            await context.SaveChangesAsync();
            return Jokecategory;
        }
    }
}
