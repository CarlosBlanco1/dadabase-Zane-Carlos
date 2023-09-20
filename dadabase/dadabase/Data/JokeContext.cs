using Microsoft.EntityFrameworkCore;

namespace dadabase.Data
{
    public class JokeContext : DbContext
    {
        public JokeContext(DbContextOptions<JokeContext> options) : base(options)
        {
        }

        public DbSet<Joke> Jokes { get; set; }
        public DbSet<Jokecategory> Categories { get; set; }
        public DbSet<Categorizedjoke> CategorizedJokes { get; set; }
        public DbSet<Audience> Audiences { get; set; }
        public DbSet<Audiencecategory> AudienceCategories { get; set; }
        public DbSet<Categorizedaudience> CategorizedAudiences { get; set; }
        public DbSet<Deliveredjoke> DeliveredJokes { get; set; }

    }
}
