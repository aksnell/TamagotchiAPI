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
        public async Task<ActionResult<IEnumerable<Tamagotchi>>> GetAllSync()
        {
            var allTamagotchis = await _context.Tamagotchis.ToListAsync();

            return Ok(allTamagotchis);
        }

    }
}
