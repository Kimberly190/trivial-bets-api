using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrivialBetsApi.Models
{
    [NotMapped]
    public class Result
    {
        public bool IsWinningGuess { get; set; }

        public int BetAmount { get; set; }

        //TODO can front end resolve this rather than returning it here?
        public int Payout { get; set; }

        public int Credit { get; set; }

        public long PlayerId { get; set; }

        public long AnswerId { get; set; }
    }
}