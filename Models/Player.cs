using System.ComponentModel.DataAnnotations.Schema;

namespace TrivialBetsApi.Models
{
    public class Player
    {
        public const int MaxPlayersPerGame = 7; 

        public long Id { get; set; }

        // User-friendly ordering (player 1, player 2, etc.), derived from ID
        [NotMapped]
        public int PlayerNumber { get; set; }

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