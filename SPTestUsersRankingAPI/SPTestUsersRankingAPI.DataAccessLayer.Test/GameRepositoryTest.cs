using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPTestUsersRankingAPI.Model;

namespace SPTestUsersRankingAPI.DataAccessLayer.Test
{
    [TestClass]
    public class GameRepositoryTest
    {
        [TestMethod]
        public void GetAll()
        {
            var gameRepository = new GameRepository(Database.Context.Instance);

            var result = gameRepository.GetAll();

            Assert.IsNotNull(result);

            Assert.IsNotNull(result.Count > 1);
        }

        [TestMethod]
        public void GetById()
        {
            var gameRepository = new GameRepository(Database.Context.Instance);

            var result = gameRepository.GetById(1);

            Assert.IsNotNull(result);

            Assert.AreEqual(1, result.Id);
        }

        [TestMethod]
        public void Create()
        {
            var gameRepository = new GameRepository(Database.Context.Instance);

            var requestObject = new Game()
            {
                Id = 0,
                Name = "Game2"
            };

            gameRepository.Create(requestObject);
        }

        [TestMethod]
        public void Update()
        {
            var gameRepository = new GameRepository(Database.Context.Instance);

            var requestObject = new Game()
            {
                Id = 1,
                Name = "Game_Updated"
            };

            var result = gameRepository.Update(requestObject);

            Assert.IsNotNull(result);

            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Game_Updated", result.Name);

            requestObject = new Game()
            {
                Id = 500,
                Name = "Game_Updated"
            };

            result = gameRepository.Update(requestObject);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Delete()
        {
            var gameRepository = new GameRepository(Database.Context.Instance);

            var requestObject = new Game()
            {
                Id = 0,
                Name = "Game2"
            };

            gameRepository.Create(requestObject);


            var result = gameRepository.Delete(new Game() { Id = 2});

            Assert.IsNotNull(result);

            result = gameRepository.Delete(new Game() { Id = -1 });

            Assert.IsNull(result);
        }
    }
}
