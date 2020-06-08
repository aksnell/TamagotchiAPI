using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
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
        public async Task<ActionResult<IEnumerable<Tamagotchi>>> GetAllAsync()
        {
            var allTamagotchis = await _context.Tamagotchis.ToListAsync();

            return Ok(allTamagotchis);
        }

        [HttpPost]
        public async Task<ActionResult<Tamagotchi>> CreateAsync(string tamaToCreate)
        {
            if (tamaToCreate == "")
            {
                var errorMessage = new
                {
                    message = "A Tamagotchi must have a name!"
                };

                return UnprocessableEntity(errorMessage);
            }

            var newTamagotchi = new Tamagotchi(tamaToCreate);

            _context.Tamagotchis.Add(newTamagotchi);
            await _context.SaveChangesAsync();

            return CreatedAtAction(null, null, newTamagotchi);

        }

        private async Task<Tamagotchi> FindTamaAsync(int id)
        {
            var foundTama = await _context.Tamagotchis.FirstOrDefaultAsync(tama => tama.Id == id);

            return foundTama;

        }
    }
}
