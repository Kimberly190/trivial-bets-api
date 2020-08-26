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
    public class ResultController : ControllerBase
    {
        private const int BEST_GUESS_CREDIT = 3;

        private readonly AppDbContext _context;

        public ResultController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Result/ForQuestion/5
        [HttpGet("ForQuestion/{questionId}")]
        public async Task<ActionResult<IEnumerable<Result>>> GetResultForQuestion(long questionId)
        {
            var question = await _context.Question.FindAsync(questionId);

            var answers = await (from answer in _context.Answer
                                 where answer.QuestionId == questionId
                                 select answer).ToListAsync();
            
            var bets = await (from bet in _context.Bet
                              from answer in _context.Answer
                              where bet.AnswerId == answer.Id
                              where answer.QuestionId == questionId
                              select bet).ToListAsync();

            var closest = (from answer in answers
                             orderby answer.Guess descending
                             where answer.Guess <= question.CorrectAnswer
                             select answer).FirstOrDefault();

            if (closest == null)
            {
                // All answers were too high.  Select the default answer.
                closest = answers.Single(a => a.Guess == -1);
            }

            var winningGuesses = from answer in answers
                              where answer.Guess == closest.Guess
                              select answer;

            return (
                from g in winningGuesses
                select new Result
                {
                    IsWinningGuess = true,
                    Credit = BEST_GUESS_CREDIT,
                    PlayerId = g.PlayerId
                }).Concat(
                    from bet in bets
                    from bg in winningGuesses
                    where bet.AnswerId == bg.Id
                    select new Result
                    {
                        Credit = bet.Amount * bet.Payout - 1, // -1: Don't credit the default chip
                        PlayerId = bet.PlayerId
                    }
                ).Concat(
                    from bet in bets
                    where !winningGuesses.Any(wg => wg.Id == bet.AnswerId)
                    select new Result
                    {
                        Credit = -1 * bet.Amount + 1, // +1: Don't debit the default chip
                        PlayerId = bet.PlayerId
                    }
                ).ToArray();
        }
    }
}