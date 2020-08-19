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
    public class GameRoomController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GameRoomController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/GameRoom
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameRoom>>> GetGameRooms()
        {
            return await _context.GameRoom.ToListAsync();
        }

        // GET: api/GameRoom/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameRoom>> GetGameRoom(long id)
        {
            var gameRoom = await _context.GameRoom.FindAsync(id);

            if (gameRoom == null)
            {
                return NotFound();
            }

            return gameRoom;
        }

        // PUT: api/GameRoom/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGameRoom(long id, GameRoom gameRoom)
        {
            if (id != gameRoom.Id)
            {
                return BadRequest();
            }

            _context.Entry(gameRoom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameRoomExists(id))
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

        // POST: api/GameRoom
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<GameRoom>> PostGameRoom(GameRoom gameRoom)
        {
            _context.GameRoom.Add(gameRoom);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGameRoom), new { id = gameRoom.Id }, gameRoom);
        }

        // DELETE: api/GameRoom/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<GameRoom>> DeleteGameRoom(long id)
        {
            var gameRoom = await _context.GameRoom.FindAsync(id);
            if (gameRoom == null)
            {
                return NotFound();
            }

            _context.GameRoom.Remove(gameRoom);
            await _context.SaveChangesAsync();

            return gameRoom;
        }

        private bool GameRoomExists(long id)
        {
            return _context.GameRoom.Any(e => e.Id == id);
        }
    }
}
