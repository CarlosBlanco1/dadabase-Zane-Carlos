using Microsoft.AspNetCore.Mvc;
using dadabase.Data;

namespace dadabase.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JokeController : Controller
    {
        private readonly ILogger<JokeController> _logger;
        private readonly IDataStore dataStore;

        public JokeController(ILogger<JokeController> logger, IDataStore dataStore)
        {
            _logger = logger;
            this.dataStore = dataStore;
        }

        [HttpGet()]
        public async Task<IEnumerable<Joke>> Get()
        {
            return await dataStore.GetAllJokes();
        }

        [HttpPost]
        public async Task<Joke> Post([FromBody] Joke joke)
        {
            var newJoke = await dataStore.AddJoke(joke);
            return newJoke;
        }

        [HttpPatch]
        public async Task<Joke> Patch([FromBody] Joke joke)
        {
            var updatedJoke = await dataStore.UpdateJoke(joke);
            return updatedJoke;
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await dataStore.DeleteJoke(id);
        }

        [HttpGet("{id}")]
        public async Task<IResult> Get(int id)
        {
            var joke = await dataStore.GetJoke(id);
            if (joke == null)
            {
                return Results.NotFound("Invalid recipe id");
            }
            /*ask about this*/return Results.Ok(joke);
            
            }
        }
}
