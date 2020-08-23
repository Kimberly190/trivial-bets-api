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
    [Route("api/[controller]")]
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
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayerForGameRoom(long gameRoomId)
        {
            var players = await (from p in _context.Player
                                where p.GameRoomId == gameRoomId
                                orderby p.Id descending
                                select p).ToListAsync();
            
            if (!players.Any())
            {
                return NotFound();
            }

            for (int i = 0; i < players.Count; i++)
            {
                players[i].PlayerNumber = i + 1;
            }

            return players;
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
