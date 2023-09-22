using Microsoft.AspNetCore.Mvc;
using dadabase.Data;

namespace dadabase.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class AudienceController : Controller
    {
        private readonly ILogger<AudienceController> _logger;
        private readonly IAudienceStore dataStore;

        public AudienceController(ILogger<AudienceController> logger, IAudienceStore dataStore)
        {
            _logger = logger;
            this.dataStore = dataStore;
        }

        [HttpGet()]
        public async Task<IEnumerable<Audience>> Get()
        {
            return await dataStore.GetAllAudiences();
        }

        [HttpPost]
        public async Task<Audience> Post([FromBody] Audience audience)
        {
            var newAudience = await dataStore.AddAudience(audience);
            return newAudience;
        }

        [HttpPatch]
        public async Task<Audience> Patch([FromBody] Audience audience)
        {
            var updatedAudience = await dataStore.UpdateAudience(audience);
            return updatedAudience;
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await dataStore.DeleteAudience(id);
        }

        [HttpGet("{id}")]
        public async Task<IResult> Get(int id)
        {
            var audience = await dataStore.GetAudience(id);
            if (audience == null)
            {
                return Results.NotFound("Invalid recipe id");
            }
            /*ask about this*/
            return Results.Ok(audience);

        }
    }
}
