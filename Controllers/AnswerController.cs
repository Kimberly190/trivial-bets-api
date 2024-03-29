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
    public class AnswerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AnswerController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Answer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Answer>>> GetAnswer()
        {
            return await _context.Answer.ToListAsync();
        }

        // GET: api/Answer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Answer>> GetAnswer(long id)
        {
            var answer = await _context.Answer.FindAsync(id);

            if (answer == null)
            {
                return NotFound();
            }

            return answer;
        }

        // GET: api/Answer/ForQuestion/5
        [HttpGet("ForQuestion/{questionId}")]
        public async Task<ActionResult<IEnumerable<Answer>>> GetAnswerForQuestion(long questionId)
        {
            //TODO move and redirect
            var answers = await (from a in _context.Answer
                                where a.QuestionId == questionId
                                select a).ToListAsync();
            
            if (!answers.Any())
            {
                return NotFound();
            }

            return answers;
        }

        // PUT: api/Answer/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnswer(long id, Answer answer)
        {
            if (id != answer.Id)
            {
                return BadRequest();
            }

            _context.Entry(answer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnswerExists(id))
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

        // POST: api/Answer
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Answer>> PostAnswer(Answer answer)
        {
            var question = _context.Question.Find(answer.QuestionId);
            if (question == null)
                return new BadRequestObjectResult("Question not found.");
            
            //https://docs.microsoft.com/en-us/ef/core/querying/related-data/explicit
            // _context.Entry(question).Collection(q => q.Answers).Load();
            // foreach (var a in question.Answers)
            // {
            //     _context.Entry(a).Collection(a => a.Bets).Load();
            // }
            //alt
            var qHasBets = _context.Entry(question).Collection(q => q.Answers)
                .Query().Where(a => a.Bets.Any()).Any();

            //TODO is this check even needed though?
            //TODO test:
            //if (question.Answers.Any(a => a.Bets.Any()))
            if (qHasBets)
                return new BadRequestObjectResult("Sorry, betting on this question is already in progress.");

            _context.Answer.Add(answer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAnswer), new { id = answer.Id }, answer);
        }

        // DELETE: api/Answer/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Answer>> DeleteAnswer(long id)
        {
            var answer = await _context.Answer.FindAsync(id);
            if (answer == null)
            {
                return NotFound();
            }

            _context.Answer.Remove(answer);
            await _context.SaveChangesAsync();

            return answer;
        }

        private bool AnswerExists(long id)
        {
            return _context.Answer.Any(e => e.Id == id);
        }
    }
}
