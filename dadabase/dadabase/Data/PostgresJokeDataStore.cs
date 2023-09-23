using Microsoft.EntityFrameworkCore;

namespace dadabase.Data
{
    public class PostgresJokeDataStore: IJokeStore
    {
        private readonly JokeContext context;
        private readonly ILogger<PostgresJokeDataStore> logger;

        public PostgresJokeDataStore(JokeContext context, ILogger<PostgresJokeDataStore> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<Joke> AddJoke(Joke Joke)
        {

            context.Jokes.Add(Joke);
            await context.SaveChangesAsync();
            return Joke;
        }

        public async Task DeleteJoke(int id)
        {
            var existingRecipe = await context.Jokes.FindAsync(id);
            if (existingRecipe is null)
            {
                throw new ArgumentException($"Recipe with id {id} does not exist");
            }
            context.Jokes.Remove(existingRecipe);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Joke>> GetAllJokes() => await context.Jokes.ToListAsync();

        public async Task<Joke> GetJoke(int id, bool showDetails = false)
        {
            if (showDetails)
            {
                return await context.Jokes
                    .Include(c => c.Categorizedjokes)
                    .ThenInclude(c => c.Jokecategory)
                    .FirstOrDefaultAsync(c => c.Id == id);
            }
            return await context.Jokes.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Joke> UpdateJoke(Joke Joke)
        {
            await Task.CompletedTask;
            var value = await context.Jokes.Include(c => c.Categorizedjokes)
                    .ThenInclude(c => c.Jokecategory)
            .FirstOrDefaultAsync(r => r.Id == Joke.Id);
            value.Jokename = Joke.Jokename;
            value.Joketext = Joke.Joketext;
            await context.SaveChangesAsync();
            return Joke;
        }

        public async Task<IEnumerable<Joke>> GetJokesByCategory(string category)
        {
            var query = from categorizedJoke in context.Categorizedjokes
                        join jokeCategory in context.Jokecategories on categorizedJoke.Jokecategoryid equals jokeCategory.Id
                        join joke in context.Jokes on categorizedJoke.Jokeid equals joke.Id
                        where jokeCategory.Categoryname == category
                        select joke;

            var results =  await query.ToListAsync();
            return results;
        }

        public async Task<IEnumerable<Joke>> GetJokesByAudience(string inputAudience)
        {
            var query = from deliveredJoke in context.Deliveredjokes
                        join audience in context.Audiences on deliveredJoke.Audienceid equals audience.Id
                        join joke in context.Jokes on deliveredJoke.Jokeid equals joke.Id
                        where audience.Audiencename == inputAudience
                        select joke;

            var results = await query.ToListAsync();
            return results;
        }

        public async Task<IEnumerable<Joke>> GetJokesByReaction()
        {
            var query = from deliveredJoke in context.Deliveredjokes
                        join jokeReactionCategory in context.Jokereactioncategories on deliveredJoke.Jokereactionid equals jokeReactionCategory.Id
                        join joke in context.Jokes on deliveredJoke.Jokeid equals joke.Id
                        orderby deliveredJoke.Jokereactionid ascending
                        select joke;

            var results = await query.ToListAsync();
            return results;
        }

        public async Task<IEnumerable<Joke>> GetJokesRankedGivenCategory(string inputCategory)
        {
            var query = from deliveredJoke in context.Deliveredjokes
                        join joke in context.Jokes on deliveredJoke.Jokeid equals joke.Id
                        join categorizedJoke in context.Categorizedjokes on joke.Id equals categorizedJoke.Jokeid 
                        join jokeCategory in context.Jokecategories on categorizedJoke.Jokecategoryid equals jokeCategory.Id
                        where jokeCategory.Categoryname == inputCategory
                        orderby deliveredJoke.Jokereactionid ascending
                        select joke;

            var results = await query.ToListAsync();
            return results;
        }

        public async Task<IEnumerable<Joke>> GetJokesRankedGivenAudience(string inputAudience)
        {
            var query = from deliveredJoke in context.Deliveredjokes
                        join joke in context.Jokes on deliveredJoke.Jokeid equals joke.Id
                        join audience in context.Audiences on deliveredJoke.Audienceid equals audience.Id
                        where audience.Audiencename == inputAudience
                        orderby deliveredJoke.Jokereactionid ascending
                        select joke;

            var results = await query.ToListAsync();
            return results;
        }

        public async Task<int> GetNumberOfTimesTold(string jokeName)
        {
            await Task.CompletedTask;
            int count = context.Deliveredjokes
                .Join(context.Jokes,
                d => d.Jokeid,
                f => f.Id,
                (d, f) => new {deliveredJokes = d, Joke = f})
                .Where(e => e.Joke.Jokename == jokeName)
                .Count();

            return count;
        }



    }
}
