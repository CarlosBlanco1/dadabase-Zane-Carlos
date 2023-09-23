using Microsoft.AspNetCore.Mvc;
using dadabase.Data;

namespace dadabase.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class JokeController : Controller
    {
        private readonly ILogger<JokeController> _logger;
        private readonly IJokeStore dataStore;

        public JokeController(ILogger<JokeController> logger, IJokeStore dataStore)
        {
            _logger = logger;
            this.dataStore = dataStore;
        }

        [HttpGet()]
        public async Task<IEnumerable<Joke>> Get()
        {
            _logger.LogInformation("GET request received for Joke controller.");
            return await dataStore.GetAllJokes();
        }

        [HttpPost]
        public async Task<Joke> Post([FromBody] Joke joke)
        {
            _logger.LogInformation("POST request received for Joke controller.");
            var newJoke = await dataStore.AddJoke(joke);
            return newJoke;
        }

        [HttpPatch]
        public async Task<Joke> Patch([FromBody] Joke joke)
        {
            _logger.LogInformation("PATCH request received for Joke controller.");
            var updatedJoke = await dataStore.UpdateJoke(joke);
            return updatedJoke;
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            _logger.LogInformation("DELETE request received for Joke controller for {id}.", id);
            await dataStore.DeleteJoke(id);
        }

        [HttpGet("{id}")]
        public async Task<IResult> Get(int id)
        {
            _logger.LogInformation("GET/id request received for Joke controller.for {id}.", id);
            var joke = await dataStore.GetJoke(id);
            if (joke == null)
            {
                return Results.NotFound("Invalid recipe id");
            }
            /*ask about this*/
            return Results.Ok(joke);

        }

        [HttpGet("category/{category}", Name = "GetJokesByCategory")]
        public async Task<IEnumerable<Joke>> GetJokesByCategory(string category)
        {
            _logger.LogInformation("GET request received for Joke controller.");
            return await dataStore.GetJokesByCategory(category);
        }

        [HttpGet("audience/{audience}", Name = "GetJokesByAudience")]
        public async Task<IEnumerable<Joke>> GetJokesByAudience(string audience)
        {
            _logger.LogInformation("GET request received for Joke controller.");
            return await dataStore.GetJokesByAudience(audience);
        }

        [HttpGet("RankedByReaction", Name = "GetJokesRankedByReaction")]
        public async Task<IEnumerable<Joke>> GetJokesByReaction()
        {
            _logger.LogInformation("GET request received for Joke controller.");
            return await dataStore.GetJokesByReaction();

        }

        [HttpGet("RankedByReactionGivenCategory/{inputCategory}", Name = "GetJokesRankedWithCategory")]
        public async Task<IEnumerable<Joke>> GetJokesByRankedByCategory(string inputCategory)
        {
            _logger.LogInformation("GET request received for Joke controller.");
            return await dataStore.GetJokesRankedGivenCategory(inputCategory);

        }

        [HttpGet("RankedByReactionGivenAudience/{inputAudience}", Name = "GetJokesRankedWithAudience")]
        public async Task<IEnumerable<Joke>> GetJokesByRankedByAudience(string inputAudience)
        {
            _logger.LogInformation("GET request received for Joke controller.");
            return await dataStore.GetJokesRankedGivenAudience(inputAudience);

        }

    }
}




    /*[HttpGet("/TimesTold/{id}")]
    public async Task<int> GetTimesTold(Joke joke)
    {

        return jokes.Count();
    }*/


