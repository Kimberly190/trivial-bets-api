using System.ComponentModel.DataAnnotations.Schema;

namespace TrivialBetsApi.Models
{
    public class Player
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public bool IsHost { get; set; }

        public int Score { get; set; }

        public long GameRoomId { get; set; }
        [ForeignKey("GameRoomId")]
        public GameRoom GameRoom { get; set; }

        //ICollection<Answer> Answers - not useful

        //ICollection<Bet> Bets - not useful
    }
}