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
            return await dataStore.GetAllJokecategorys();
        }

        [HttpPost]
        public async Task<Jokecategory> Post([FromBody] Jokecategory jokecategory)
        {
            var newJokecategory = await dataStore.AddJokecategory(jokecategory);
            return newJokecategory;
        }

        [HttpPatch]
        public async Task<Jokecategory> Patch([FromBody] Jokecategory jokecategory)
        {
            var updatedJokecategory = await dataStore.UpdateJokecategory(jokecategory);
            return updatedJokecategory;
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await dataStore.DeleteJokecategory(id);
        }

        [HttpGet("{id}")]
        public async Task<IResult> Get(int id)
        {
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

