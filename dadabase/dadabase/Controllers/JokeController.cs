﻿using Microsoft.AspNetCore.Mvc;
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

        /*[HttpGet("/TimesTold/{id}")]
        public async Task<int> GetTimesTold(Joke joke)
        {

            return jokes.Count();
        }*/

        [HttpGet("{id}")]
        public async Task<IResult> Get(int id)
        {
            _logger.LogInformation("GET/id request received for Joke controller.for {id}.", id);
            var joke = await dataStore.GetJoke(id);
            if (joke == null)
            {
                return Results.NotFound("Invalid recipe id");
            }
            /*ask about this*/return Results.Ok(joke);
            
            }
        }
}
