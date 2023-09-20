﻿using Microsoft.EntityFrameworkCore;

namespace dadabase.Data
{
    public class PostgresDataStore: IDataStore
    {
        private readonly JokeContext context;
        private readonly ILogger<PostgresDataStore> logger;

        public PostgresDataStore(JokeContext context, ILogger<PostgresDataStore> logger)
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
            //ask about changing the category and delivery info as well
            await context.SaveChangesAsync();
            return Joke;
        }

        /*public async Task CategorizeJoke(Joke Joke, Category category)
        {
            context.CategorizedJokes.Add(new CategorizedJoke { JokeId = Joke.Id, CategoryId = category.Id });
            await context.SaveChangesAsync();
        }*/

        /* public async Task<Category> AddCategory(Category category)
         {
             context.Categories.Add(category);
             await context.SaveChangesAsync();
             return category;
         }*/

    }
}
