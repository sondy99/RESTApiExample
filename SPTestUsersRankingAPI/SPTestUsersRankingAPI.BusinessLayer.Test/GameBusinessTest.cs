using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPTestUsersRankingAPI.DTO;

namespace SPTestUsersRankingAPI.BusinessLayer.Test
{
    [TestClass]
    public class GameBusinessTest
    {

        [TestMethod]
        public void GetAll()
        {
            var gameBusiness = new GameBusiness();

            var result = gameBusiness.GetAll();
            
            Assert.IsNotNull(result);

            Assert.IsNotNull(result.Count > 1);
        }

        [TestMethod]
        public void GetById()
        {
            var gameBusiness = new GameBusiness();

            var result = gameBusiness.GetById(1);
            
            Assert.IsNotNull(result);

            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Game", result.Name);
        }

        [TestMethod]
        public void GetAbsoluteRanking()
        {
            var gameBusiness = new GameBusiness();

            var result = gameBusiness.GetAbsoluteRanking(10);
            
            Assert.IsNotNull(result);

            Assert.IsNotNull(result.Count == 10);

            Assert.IsNotNull(result.First().Position < result.Last().Position
                && result.First().Score >= result.Last().Score);

            result = gameBusiness.GetAbsoluteRanking(100);
            
            Assert.IsNotNull(result);

            Assert.IsNotNull(result.Count == 100);

            Assert.IsNotNull(result.First().Position < result.Last().Position
                && result.First().Score >= result.Last().Score);
        }

        [TestMethod]
        public void GetRelativeRanking()
        {
            var gameBusiness = new GameBusiness();

            var result = gameBusiness.GetRelativeRanking(10, 2);
            
            Assert.IsNotNull(result);

            Assert.IsNotNull(result.Count == 5);

            Assert.IsNotNull(result.First().Position == 7
                && result.Last().Position == 12);

            Assert.IsNotNull(result.First().Position < result.Last().Position
                && result.First().Score >= result.Last().Score);

            result = gameBusiness.GetRelativeRanking(100, 10);
            
            Assert.IsNotNull(result.Count == 21);

            Assert.IsNotNull(result.First().Position == 89
                && result.Last().Position == 111);

            Assert.IsNotNull(result.First().Position < result.Last().Position
                && result.First().Score >= result.Last().Score);
        }
        
        [TestMethod]
        public void Create()
        {
            var gameBusiness = new GameBusiness();

            var requestObject = new GameDTO()
            {
                Id = 0,
                Name = "Game2"
            };
            
            gameBusiness.Create(requestObject);
        }

        [TestMethod]
        public void Update()
        {
            var gameBusiness = new GameBusiness();

            var requestObject = new GameDTO()
            {
                Id = 1,
                Name = "Game_Updated"
            };
            
            var result = gameBusiness.Update(requestObject);

            Assert.IsNotNull(result);

            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Game_Updated", result.Name);

            requestObject = new GameDTO()
            {
                Id = 500,
                Name = "Game_Updated"
            };
            
            result = gameBusiness.Update(requestObject);

            Assert.IsNull(result);
        }


        [TestMethod]
        public void Delete()
        {
            var gameBusiness = new GameBusiness();

            var result = gameBusiness.Delete(2);

            Assert.IsNotNull(result);
            
            result = gameBusiness.Delete(-1);

            Assert.IsNull(result);
        }
    }
}
