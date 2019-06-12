using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPTestUsersRankingAPI.Model;
using System.Linq;

namespace SPTestUsersRankingAPI.DataAccessLayer.Test
{
    [TestClass]
    public class UserGameRepositoryTest
    {
        [TestMethod]
        public void GetById()
        {
            var userGameRepository = new UserGameRepository(Database.Context.Instance);

            var result = userGameRepository.GetById(1);

            Assert.IsNotNull(result);

            Assert.AreEqual(1, result.Id);
        }

        [TestMethod]
        public void GetAbsoluteRanking()
        {
            var userGameRepository = new UserGameRepository(Database.Context.Instance);

            var result = userGameRepository.GetAbsoluteRanking(10);

            Assert.IsNotNull(result);

            Assert.IsNotNull(result.Count == 10);

            Assert.IsNotNull(result.First().Score >= result.Last().Score);

            result = userGameRepository.GetAbsoluteRanking(100);

            Assert.IsNotNull(result);

            Assert.IsNotNull(result.Count == 100);

            Assert.IsNotNull(result.First().Score >= result.Last().Score);
        }

        [TestMethod]
        public void GetRelativeRanking()
        {
            var userGameRepository = new UserGameRepository(Database.Context.Instance);

            var result = userGameRepository.GetRelativeRanking(10, 2);

            Assert.IsNotNull(result);

            Assert.IsNotNull(result.Count == 5);
            
            Assert.IsNotNull(result.First().Score >= result.Last().Score);

            result = userGameRepository.GetRelativeRanking(100, 10);

            Assert.IsNotNull(result.Count == 21);

            Assert.IsNotNull(result.First().Score >= result.Last().Score);
        }

        [TestMethod]
        public void Create()
        {
            var userGameRepository = new UserGameRepository(Database.Context.Instance);

            var game = new Game()
            {
                Id = 1,
                Name = "Game"
            };

            var user = new User()
            {
                Id = 0,
                FirstName = "Daniel",
                LastName = "Cardoza",
            };
            
            var userGame = new UserGame()
            {
                Id = 0,
                UserId = user.Id,
                User = user,
                GameId = game.Id,
                Game = game,
                Score = 1000
            };

            userGameRepository.Create(userGame);
        }

        [TestMethod]
        public void Update()
        {
            var userGameRepository = new UserGameRepository(Database.Context.Instance);

            var game = new Game()
            {
                Id = 1,
                Name = "Game"
            };

            var user = new User()
            {
                Id = 1,
                FirstName = "FirstName_1",
                LastName = "LastName_1",
            };

            var userGame = new UserGame()
            {
                Id = 1,
                UserId = user.Id,
                User = user,
                GameId = game.Id,
                Game = game,
                Score = 500
            };
            
            var result = userGameRepository.Update(userGame);

            Assert.IsNotNull(result);

            Assert.AreEqual(1, result.Id);
            Assert.AreEqual(1, result.UserId);
            Assert.AreEqual("FirstName_1", result.User.FirstName);
            Assert.AreEqual("LastName_1", result.User.LastName);
            Assert.AreEqual(1, result.GameId);
            Assert.AreEqual("Game", result.Game.Name);
            Assert.AreEqual(500, result.Score);
        }

        [TestMethod]
        public void Delete()
        {
            var userGameRepository = new UserGameRepository(Database.Context.Instance);

            var result = userGameRepository.Delete(new UserGame() { Id = 100});

            Assert.IsNotNull(result);

            result = userGameRepository.Delete(new UserGame() { Id = -1 });

            Assert.IsNull(result);
        }
    }
}
