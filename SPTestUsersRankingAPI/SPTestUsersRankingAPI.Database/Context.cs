using SPTestUsersRankingAPI.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace SPTestUsersRankingAPI.Database
{
    public class Context : IContext
    {

        private static Context instance;
        public static Context Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Context();
                }
                return instance;
            }
        }

        public ConcurrentDictionary<int, User> Users { get; } = new ConcurrentDictionary<int, User>();
        public ConcurrentDictionary<int, UserGame> UserGames { get; } = new ConcurrentDictionary<int, UserGame>();
        public ConcurrentDictionary<int, Game> Games { get; } = new ConcurrentDictionary<int, Game>();

        private Context()
        {
            InitialData();
        }

        private void InitialData()
        {
            var game = new Game()
            {
                Id = 1,
                Name = "Game"
            };

            Games.TryAdd(1, game);

            Random random = new Random();

            for (int i = 1; i <= 200; i++)
            {
                var user = new User()
                {
                    Id = i,
                    FirstName = string.Format("FirstName_{0}", i),
                    LastName = string.Format("LastName_{0}", i),
                };

                int randomScore = random.Next(0, 1000);

                var userGame = new UserGame()
                {
                    Id = i,
                    UserId = user.Id,
                    User = user,
                    GameId = game.Id,
                    Game = game,
                    Score = randomScore
                };

                user.Games = new List<UserGame>() { userGame };

                UserGames.TryAdd(userGame.Id, userGame);
                Users.TryAdd(user.Id, user);

            }
        }
    }
}
