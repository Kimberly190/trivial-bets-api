using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace TrivialBetsApi.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<AppDbContext>>()))
            {
                // Look for any data.
                if (context.GameRoom.Any())
                {
                    return; // DB has been seeded.
                }

                var gameRoom = context.GameRoom.Add(
                    new GameRoom
                    {
                    }
                );

                AddPlayers(context, gameRoom);

                AddQuestion1(context, gameRoom);

                AddQuestion2(context, gameRoom);

                AddQuestion3(context, gameRoom);

                context.SaveChanges();
            }
        }

        private static void AddPlayers(AppDbContext context, Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<GameRoom> gameRoom)
        {
            context.Player.AddRange(
                new Player
                {
                    Name = "Anna",
                    IsHost = true,
                    GameRoom = gameRoom.Entity
                },
                new Player
                {
                    Name = "Bob",
                    GameRoom = gameRoom.Entity
                },
                new Player
                {
                    Name = "Chen",
                    GameRoom = gameRoom.Entity
                }
            );
        }

        private static void AddQuestion1(AppDbContext context, Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<GameRoom> gameRoom)
        {
            var question = context.Question.Add(
                new Question
                {
                    GameRoom = gameRoom.Entity,
                    CorrectAnswer = 45,
                    Rank = 1
                }
            );

            context.Answer.AddRange(
                new Answer
                {
                    Guess = -1,
                    Question = question.Entity
                },
                new Answer
                {
                    Guess = 30,
                    Question = question.Entity,
                    PlayerId = 1
                },
                new Answer
                {
                    Guess = 90,
                    Question = question.Entity,
                    PlayerId = 2
                },
                new Answer
                {
                    Guess = 40,
                    Question = question.Entity,
                    PlayerId = 3
                }
            );

            context.Bet.AddRange(
                new Bet
                {
                    Amount = 1,
                    Payout = 5,
                    PlayerId = 1,
                    AnswerId = 2
                },
                new Bet
                {
                    Amount = 1,
                    Payout = 3,
                    PlayerId = 2,
                    AnswerId = 4
                },
                new Bet
                {
                    Amount = 1,
                    Payout = 3,
                    PlayerId = 3,
                    AnswerId = 4
                },
                new Bet
                {
                    Amount = 1,
                    Payout = 5,
                    PlayerId = 1,
                    AnswerId = 2
                },
                new Bet
                {
                    Amount = 1,
                    Payout = 6,
                    PlayerId = 2,
                    AnswerId = 1
                },
                new Bet
                {
                    Amount = 1,
                    Payout = 3,
                    PlayerId = 3,
                    AnswerId = 4
                }
            );
        }
    
        private static void AddQuestion2(AppDbContext context, Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<GameRoom> gameRoom)
        {
            var question = context.Question.Add(
                new Question
                {
                    GameRoom = gameRoom.Entity,
                    CorrectAnswer = 20,
                    Rank = 2
                }
            );

            context.Answer.AddRange(
                new Answer
                {
                    Guess = -1,
                    Question = question.Entity
                },
                new Answer
                {
                    Guess = 70,
                    Question = question.Entity,
                    PlayerId = 1
                },
                new Answer
                {
                    Guess = 60,
                    Question = question.Entity,
                    PlayerId = 2
                },
                new Answer
                {
                    Guess = 50,
                    Question = question.Entity,
                    PlayerId = 3
                }
            );

            context.Bet.AddRange(
                new Bet
                {
                    Amount = 1,
                    Payout = 5,
                    PlayerId = 1,
                    AnswerId = 6
                },
                new Bet
                {
                    Amount = 1,
                    Payout = 3,
                    PlayerId = 2,
                    AnswerId = 8
                },
                new Bet
                {
                    Amount = 3,
                    Payout = 4,
                    PlayerId = 3,
                    AnswerId = 8
                },
                new Bet
                {
                    Amount = 1,
                    Payout = 5,
                    PlayerId = 1,
                    AnswerId = 6
                },
                new Bet
                {
                    Amount = 1,
                    Payout = 6,
                    PlayerId = 2,
                    AnswerId = 5
                },
                new Bet
                {
                    Amount = 1,
                    Payout = 3,
                    PlayerId = 3,
                    AnswerId = 8
                }
            );
        }

        private static void AddQuestion3(AppDbContext context, Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<GameRoom> gameRoom)
        {
            var question = context.Question.Add(
                new Question
                {
                    GameRoom = gameRoom.Entity,
                    CorrectAnswer = 45,
                    Rank = 3
                }
            );

            context.Answer.AddRange(
                new Answer
                {
                    Guess = -1,
                    Question = question.Entity
                },
                new Answer
                {
                    Guess = 40,
                    Question = question.Entity,
                    PlayerId = 1
                },
                new Answer
                {
                    Guess = 40,
                    Question = question.Entity,
                    PlayerId = 2
                },
                new Answer
                {
                    Guess = 50,
                    Question = question.Entity,
                    PlayerId = 3
                }
            );

            context.Bet.AddRange(
                new Bet
                {
                    Amount = 1,
                    Payout = 5,
                    PlayerId = 1,
                    AnswerId = 10
                },
                new Bet
                {
                    Amount = 1,
                    Payout = 3,
                    PlayerId = 2,
                    AnswerId = 12
                },
                new Bet
                {
                    Amount = 3,
                    Payout = 5,
                    PlayerId = 3,
                    AnswerId = 10
                },
                new Bet
                {
                    Amount = 1,
                    Payout = 5,
                    PlayerId = 1,
                    AnswerId = 10
                },
                new Bet
                {
                    Amount = 1,
                    Payout = 6,
                    PlayerId = 2,
                    AnswerId = 9
                },
                new Bet
                {
                    Amount = 1,
                    Payout = 3,
                    PlayerId = 3,
                    AnswerId = 12
                }
            );
        }
    }
}