using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace TrivialBetsApi.Models
{
    public class Answer
    {
        public long Id { get; set; }

        public decimal Guess { get; set; }

        public long PlayerId { get; set; }
        [ForeignKey("PlayerId")]
        public Player Player { get; set; }

        public long QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public Question Question { get; set; }

        public ICollection<Bet> Bets { get; set; }
    }
}