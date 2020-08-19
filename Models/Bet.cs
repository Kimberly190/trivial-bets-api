using System.ComponentModel.DataAnnotations.Schema;

namespace TrivialBetsApi.Models
{
    public class Bet
    {
        public long Id { get; set; }

        public int Amount { get; set; }

        public long PlayerId { get; set; }
        [ForeignKey("PlayerId")]
        public Player Player { get; set; }

        public long AnswerId { get; set; }
        [ForeignKey("AnswerId")]
        public Answer Answer { get; set; }
    }   
}