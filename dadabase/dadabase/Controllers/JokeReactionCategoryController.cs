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
            return await dataStore.GetAllJokereactioncategorys();
        }

        [HttpPost]
        public async Task<Jokereactioncategory> Post([FromBody] Jokereactioncategory jokereactioncategory)
        {
            var newJokereactioncategory = await dataStore.AddJokereactioncategory(jokereactioncategory);
            return newJokereactioncategory;
        }

        [HttpPatch]
        public async Task<Jokereactioncategory> Patch([FromBody] Jokereactioncategory jokereactioncategory)
        {
            var updatedJokereactioncategory = await dataStore.UpdateJokereactioncategory(jokereactioncategory);
            return updatedJokereactioncategory;
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await dataStore.DeleteJokereactioncategory(id);
        }

        [HttpGet("{id}")]
        public async Task<IResult> Get(int id)
        {
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

