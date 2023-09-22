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
            _logger.LogInformation("GET request received for Audience controller.");
            return await dataStore.GetAllAudiences();
        }

        [HttpPost]
        public async Task<Audience> Post([FromBody] Audience audience)
        {
            _logger.LogInformation("POST request received for Audience controller.");
            var newAudience = await dataStore.AddAudience(audience);
            return newAudience;
        }

        [HttpPatch]
        public async Task<Audience> Patch([FromBody] Audience audience)
        {
            _logger.LogInformation("PATCH request received for Audience controller.");
            var updatedAudience = await dataStore.UpdateAudience(audience);
            return updatedAudience;
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            _logger.LogInformation("DELETE request received for Audience controller.for {id}.", id);
            await dataStore.DeleteAudience(id);
        }

        [HttpGet("{id}")]
        public async Task<IResult> Get(int id)
        {
            _logger.LogInformation("GET/id request received for Audience controller.for {id}.", id);
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
