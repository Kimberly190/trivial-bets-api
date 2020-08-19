using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace TrivialBetsApi.Models
{
    public class Question
    {
        public long Id { get; set; }

        public decimal CorrectAnswer { get; set; }

        public long GameRoomId { get; set; }
        [ForeignKey("GameRoomId")]
        public GameRoom GameRoom { get; set; }

        public ICollection<Answer> Answers { get; set; }
    }
}