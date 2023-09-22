using dadabase.Data;
using Microsoft.AspNetCore.Mvc;

namespace dadabase.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class JokeCategoryController : Controller
    {
        private readonly ILogger<JokeController> _logger;
        private readonly IJokeCategoryStore dataStore;

        public JokeCategoryController(ILogger<JokeController> logger, IJokeCategoryStore dataStore)
        {
            _logger = logger;
            this.dataStore = dataStore;
        }

        [HttpGet()]
        public async Task<IEnumerable<Jokecategory>> Get()
        {
            _logger.LogInformation("GET request received for JokeCategory controller.");
            return await dataStore.GetAllJokecategorys();
        }

        [HttpPost]
        public async Task<Jokecategory> Post([FromBody] Jokecategory jokecategory)
        {
            _logger.LogInformation("POST request received for JokeCategory controller.");
            var newJokecategory = await dataStore.AddJokecategory(jokecategory);
            return newJokecategory;
        }

        [HttpPatch]
        public async Task<Jokecategory> Patch([FromBody] Jokecategory jokecategory)
        {
            _logger.LogInformation("PATCH request received for JokeCategory controller.");
            var updatedJokecategory = await dataStore.UpdateJokecategory(jokecategory);
            return updatedJokecategory;
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            _logger.LogInformation("DELETE request received for JokeCategory controller.");
            await dataStore.DeleteJokecategory(id);
        }

        [HttpGet("{id}")]
        public async Task<IResult> Get(int id)
        {
            _logger.LogInformation("GET/id request received for JokeCategory controller.");
            var joke = await dataStore.GetJokecategory(id);
            if (joke == null)
            {
                return Results.NotFound("Invalid recipe id");
            }
            /*ask about this*/
            return Results.Ok(joke);

        }
    }

}

