using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrivialBetsApi.Models;

namespace TrivialBetsApi.Controllers
{
    [ApiVersion( "1.0" )]
    [ApiVersion( "2.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PlayerController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Player
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayer()
        {
            return await _context.Player.ToListAsync();
        }

        // GET: api/Player/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(long id)
        {
            var player = await _context.Player.FindAsync(id);

            if (player == null)
            {
                return NotFound();
            }

            return player;
        }

        // GET: api/Player/ForGameRoom/5
        [HttpGet("ForGameRoom/{gameRoomId}")]
        public ActionResult GetPlayerForGameRoom(long gameRoomId)
        {
            //TODO update old version
            return Redirect($"~/api/v2/GameRoom/{gameRoomId}/Player");
        }

        // PUT: api/Player/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayer(long id, Player player)
        {
            if (id != player.Id)
            {
                return BadRequest();
            }

            _context.Entry(player).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Player
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer(Player player)
        {
            var gameRoom = _context.GameRoom.Find(player.GameRoomId);
            //TODO test:
            if (gameRoom == null)
                return new NotFoundObjectResult("Sorry, that Game Room was not found.");
            //TODO test:
            if (_context.Question.Any(q => q.GameRoomId == gameRoom.Id))
                return new BadRequestObjectResult("Sorry, that game was already started.");
            //TODO test: //TODO why is gameRoom.Players null, can load it?
            if (_context.Player.Count(p => p.GameRoomId == gameRoom.Id) >= 7)
                return new BadRequestObjectResult("Sorry, that game is full.");

            _context.Player.Add(player);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPlayer), new { id = player.Id }, player);
        }

        // DELETE: api/Player/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Player>> DeletePlayer(long id)
        {
            var player = await _context.Player.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            _context.Player.Remove(player);
            await _context.SaveChangesAsync();

            return player;
        }

        private bool PlayerExists(long id)
        {
            return _context.Player.Any(e => e.Id == id);
        }
    }
}
