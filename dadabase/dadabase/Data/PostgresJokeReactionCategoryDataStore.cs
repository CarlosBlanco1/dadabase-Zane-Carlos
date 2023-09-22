using Microsoft.EntityFrameworkCore;

namespace dadabase.Data
{
    public class PostgresJokeReactionCategoryDataStore : IJokereactioncategoryStore
    {
        private readonly JokeContext context;
        private readonly ILogger<PostgresJokeDataStore> logger;

        public PostgresJokeReactionCategoryDataStore(JokeContext context, ILogger<PostgresJokeDataStore> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<Jokereactioncategory> AddJokereactioncategory(Jokereactioncategory Jokereactioncategory)
        {
            context.Jokereactions.Add(Jokereactioncategory);
            await context.SaveChangesAsync();
            return Jokereactioncategory;
        }

        public async Task DeleteJokereactioncategory(int id)
        {
            var existingRecipe = await context.Categories.FindAsync(id);
            if (existingRecipe is null)
            {
                throw new ArgumentException($"Recipe with id {id} does not exist");
            }
            context.Categories.Remove(existingRecipe);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Jokereactioncategory>> GetAllJokereactioncategorys() => await context.Jokereactions.ToListAsync();

        public async Task<Jokereactioncategory> GetJokereactioncategory(int id, bool showDetails = false)
        {
            if (showDetails)
            {
                return await context.Jokereactions
                    .Include(c => c.Deliveredjokes)
                    .ThenInclude(c => c.Jokereaction)
                    .FirstOrDefaultAsync(c => c.Id == id);
            }
            return await context.Jokereactions.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Jokereactioncategory> UpdateJokereactioncategory(Jokereactioncategory Jokereactioncategory)
        {
            await Task.CompletedTask;
            var value = await context.Jokereactions.Include(c => c.Deliveredjokes)
                    .ThenInclude(c => c.Jokereaction)
            .FirstOrDefaultAsync(r => r.Id == Jokereactioncategory.Id);
            value.Description = Jokereactioncategory.Description;
            //ask about changing the category and delivery info as well
            await context.SaveChangesAsync();
            return Jokereactioncategory;
        }

    }
}
