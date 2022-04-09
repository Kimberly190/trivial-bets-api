using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace TrivialBetsApi.Models
{
    public class Question
    {
        public long Id { get; set; }

        public decimal? CorrectAnswer { get; set; } = null;

        public long GameRoomId { get; set; }
        [ForeignKey("GameRoomId")]
        public GameRoom GameRoom { get; set; }

        public int Rank { get; set; }

        public ICollection<Answer> Answers { get; set; }
    }
}