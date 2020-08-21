using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrivialBetsApi.Models
{
    public class Result
    {
        public bool IsBestGuess { get; set; }

        public int Credit { get; set; }

        public long PlayerId { get; set; }
        [ForeignKey("PlayerId")]
        public Player Player { get; set; }
    }
}