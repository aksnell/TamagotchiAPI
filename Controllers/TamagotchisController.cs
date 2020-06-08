using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TamagotchiAPI.Models;

namespace TamagotchiAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TamagotchisController : ControllerBase
    {

        private readonly DatabaseContext _context;

        public TamagotchisController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tamagotchi>>> GetAllAsync(bool? livingOnly = null)
        {
            List<Tamagotchi> allTamagotchis;
            if (livingOnly == true)
            {
                allTamagotchis = _context.Tamagotchis.AsEnumerable().Where(tama => tama.IsDead == false).ToList();
            } else {
                allTamagotchis = await _context.Tamagotchis.ToListAsync();
            }

            return Ok(allTamagotchis);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tamagotchi>> GetByIDAsync(int id)
        {
            var tama = await FindTamaAsync(id);

            if (tama == null)
            {
                return NotFound();
            }

            return Ok(tama);
        }

        [HttpPost]
        public async Task<ActionResult<Tamagotchi>> CreateAsync([FromBody]NewTamagotchi tamaToCreate)
        {
            if (tamaToCreate.Name == "")
            {
                var errorMessage = new
                {
                    message = "A Tamagotchi must have a name!"
                };

                return UnprocessableEntity(errorMessage);
            }

            var newTamagotchi = new Tamagotchi(tamaToCreate.Name);

            _context.Tamagotchis.Add(newTamagotchi);
            await _context.SaveChangesAsync();

            return CreatedAtAction(null, null, newTamagotchi);
        }

        [HttpPost("{id}/playtimes")]
        public async Task<ActionResult<Tamagotchi>> PlayAsync(int id)
        {
            var tama = await FindTamaAsync(id);

            if (tama == null)
            {
                return NotFound();
            }

            if (!tama.IsDead)
            {
                tama.HappinessLevel += 5;
                tama.HungerLevel += 3;
                await UpdateTamaAsync(tama);
            }

            return Ok(tama);
        }

        [HttpPost("{id}/feedings")]
        public async Task<ActionResult<Tamagotchi>> FeedAsync(int id)
        {
            var tama = await FindTamaAsync(id);

            if (tama == null)
            {
                return NotFound();
            }

            if (!tama.IsDead)
            {
                tama.HungerLevel -= 5;
                tama.HappinessLevel -= 3;
                await UpdateTamaAsync(tama);
            }

            return Ok(tama);
        }

        [HttpPost("{id}/scoldings")]
        public async Task<ActionResult<Tamagotchi>> ScoldAsync(int id)
        {
            var tama = await FindTamaAsync(id);

            if (tama == null)
            {
                return NotFound();
            }

            if (!tama.IsDead)
            {
                tama.HappinessLevel -= 5;
                await UpdateTamaAsync(tama);
            }

            return Ok(tama);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Tamagotchi>> DeleteAsync(int id)
        {
            var tama = await FindTamaAsync(id);

            if (tama == null)
            {
                return NotFound();
            }

            _context.Tamagotchis.Remove(tama);
            await _context.SaveChangesAsync();

            return Ok(tama);
        }

        private async Task UpdateTamaAsync(Tamagotchi tama)
        {
            tama.LastInteractedWith = DateTime.Now;

            _context.Entry(tama).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        private async Task<Tamagotchi> FindTamaAsync(int id)
        {
            var foundTama = await _context.Tamagotchis.FirstOrDefaultAsync(tama => tama.Id == id);

            return foundTama;

        }

        public class NewTamagotchi
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }
        }
    }
}
