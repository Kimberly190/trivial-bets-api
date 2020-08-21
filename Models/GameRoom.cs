using System.Collections.Generic;

namespace TrivialBetsApi.Models
{
    public class GameRoom
    {
        public long Id { get; set; }

        public ICollection<Player> Players { get; set; }

        public ICollection<Question> Questions { get; set; }
    }    
}