using Microsoft.EntityFrameworkCore;
using TrivialBetsApi.Models;

namespace TrivialBetsApi.Models
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<TrivialBetsApi.Models.GameRoom> GameRoom { get; set; }

        public DbSet<TrivialBetsApi.Models.Question> Question { get; set; }

        public DbSet<TrivialBetsApi.Models.Answer> Answer { get; set; }

        public DbSet<TrivialBetsApi.Models.Player> Player { get; set; }

        public DbSet<TrivialBetsApi.Models.Bet> Bet { get; set; }
    }
}