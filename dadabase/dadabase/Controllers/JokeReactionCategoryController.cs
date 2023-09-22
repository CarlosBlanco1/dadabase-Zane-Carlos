using dadabase.Data;
using Microsoft.AspNetCore.Mvc;

namespace dadabase.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class JokeReactionCategoryController : Controller
    {
        private readonly ILogger<JokeController> _logger;
        private readonly IJokereactioncategoryStore dataStore;

        public JokeReactionCategoryController(ILogger<JokeController> logger, IJokereactioncategoryStore dataStore)
        {
            _logger = logger;
            this.dataStore = dataStore;
        }

        [HttpGet()]
        public async Task<IEnumerable<Jokereactioncategory>> Get()
        {
            _logger.LogInformation("GET request received for JokeReactionCategory controller.");
            return await dataStore.GetAllJokereactioncategorys();
        }

        [HttpPost]
        public async Task<Jokereactioncategory> Post([FromBody] Jokereactioncategory jokereactioncategory)
        {
            _logger.LogInformation("POST request received for JokeReactionCategory controller.");
            var newJokereactioncategory = await dataStore.AddJokereactioncategory(jokereactioncategory);
            return newJokereactioncategory;
        }

        [HttpPatch]
        public async Task<Jokereactioncategory> Patch([FromBody] Jokereactioncategory jokereactioncategory)
        {
            _logger.LogInformation("PATCH request received for JokeReactionCategory controller.");
            var updatedJokereactioncategory = await dataStore.UpdateJokereactioncategory(jokereactioncategory);
            return updatedJokereactioncategory;
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            _logger.LogInformation("DELETE request received for JokeReactionCategory controller.for {id}.", id);
            await dataStore.DeleteJokereactioncategory(id);
        }

        [HttpGet("{id}")]
        public async Task<IResult> Get(int id)
        {
            _logger.LogInformation("GET/id request received for JokeReactionCategory controller.for {id}.", id);
            var joke = await dataStore.GetJokereactioncategory(id);
            if (joke == null)
            {
                return Results.NotFound("Invalid recipe id");
            }
            /*ask about this*/
            return Results.Ok(joke);

        }
    }
}

