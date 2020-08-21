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
    public class BetController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BetController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Bet
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bet>>> GetBet()
        {
            return await _context.Bet.ToListAsync();
        }

        // GET: api/Bet/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bet>> GetBet(long id)
        {
            var bet = await _context.Bet.FindAsync(id);

            if (bet == null)
            {
                return NotFound();
            }

            return bet;
        }

        // GET: api/Bet/ForQuestion/5
        [HttpGet("ForQuestion/{questionId}")]
        public async Task<ActionResult<IEnumerable<Bet>>> GetBetForQuestion(long questionId)
        {
            var bets = await (from b in _context.Bet
                                where b.Answer.QuestionId == questionId
                                select b).ToListAsync();
            
            if (!bets.Any())
            {
                return NotFound();
            }

            return bets;
        }

        // PUT: api/Bet/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBet(long id, Bet bet)
        {
            if (id != bet.Id)
            {
                return BadRequest();
            }

            _context.Entry(bet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BetExists(id))
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

        // POST: api/Bet
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Bet>> PostBet(Bet bet)
        {
            _context.Bet.Add(bet);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBet), new { id = bet.Id }, bet);
        }

        // DELETE: api/Bet/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Bet>> DeleteBet(long id)
        {
            var bet = await _context.Bet.FindAsync(id);
            if (bet == null)
            {
                return NotFound();
            }

            _context.Bet.Remove(bet);
            await _context.SaveChangesAsync();

            return bet;
        }

        private bool BetExists(long id)
        {
            return _context.Bet.Any(e => e.Id == id);
        }
    }
}
