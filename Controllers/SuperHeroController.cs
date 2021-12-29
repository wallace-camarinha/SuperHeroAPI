using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/superheroes")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext context;

        public SuperHeroController(DataContext context)
        {
            this.context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetHeroes()
        {
            var dbHeroes = await context.SuperHeroes.ToListAsync();

            return Ok(dbHeroes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetHero(int id)
        {
            var hero = await context.SuperHeroes.FindAsync(id);

            if (hero == null)
                return BadRequest("Hero not found!");

            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<SuperHero>> AddHero(SuperHero hero)
        {
            var dbHero = context.SuperHeroes.Add(hero);
            await context.SaveChangesAsync();

            return Ok(await context.SuperHeroes.FindAsync(dbHero.Entity.Id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SuperHero>> UpdateHero(int id, SuperHero request)
        {
            var dbHero = await context.SuperHeroes.FindAsync(id);

            if (dbHero == null)
                return BadRequest("Hero not found!");

            dbHero.Name = request.Name;
            dbHero.FirstName = request.FirstName;
            dbHero.LastName = request.LastName;
            dbHero.Place = request.Place;

            await context.SaveChangesAsync();

            return Ok(dbHero);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var dbHero = await context.SuperHeroes.FindAsync(id);

            if (dbHero == null)
                return BadRequest("Hero not found!");

            context.SuperHeroes.Remove(dbHero);
            await context.SaveChangesAsync();

            return Ok(await context.SuperHeroes.ToListAsync());
        }

    }
}
