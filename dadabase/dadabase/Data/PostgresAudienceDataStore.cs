using Microsoft.EntityFrameworkCore;

namespace dadabase.Data
{
    public class PostgresAudienceDataStore: IAudienceStore
    {
        private readonly JokeContext context;
        private readonly ILogger<PostgresJokeDataStore> logger;

        public PostgresAudienceDataStore(JokeContext context, ILogger<PostgresJokeDataStore> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<Audience> AddAudience(Audience Audience)
        {
            context.Audiences.Add(Audience);
            await context.SaveChangesAsync();
            return Audience;
        }

        public async Task DeleteAudience(int id)
        {
            var existingRecipe = await context.Audiences.FindAsync(id);
            if (existingRecipe is null)
            {
                throw new ArgumentException($"Recipe with id {id} does not exist");
            }
            context.Audiences.Remove(existingRecipe);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Audience>> GetAllAudiences() => await context.Audiences.ToListAsync();

        public async Task<Audience> GetAudience(int id, bool showDetails = false)
        {
            if (showDetails)
            {
                return await context.Audiences
                    .Include(c => c.Categorizedaudiences)
                    .ThenInclude(c => c.Audiencecategory)
                    .FirstOrDefaultAsync(c => c.Id == id);
            }
            return await context.Audiences.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Audience> UpdateAudience(Audience Audience)
        {
            await Task.CompletedTask;
            var value = await context.Audiences.Include(c => c.Categorizedaudiences)
                    .ThenInclude(c => c.Audiencecategory)
            .FirstOrDefaultAsync(r => r.Id == Audience.Id);
            value.Audiencename = Audience.Audiencename;
            //ask about changing the category and delivery info as well
            await context.SaveChangesAsync();
            return Audience;
        }



    }

}

