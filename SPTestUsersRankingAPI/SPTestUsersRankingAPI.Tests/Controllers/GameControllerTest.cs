
using System.Net.Http;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPTestUsersRankingAPI.Controllers;
using System.Linq;
using System.Collections.Generic;
using SPTestUsersRankingAPI.DTO;
using System.Net;

namespace SPTestUsersRankingAPI.Tests.Controllers
{

    [TestClass]
    public class GameControllerTest
    {
        private GameController SetController()
        {
            GameController controller = new GameController();
            controller.Request = new HttpRequestMessage();
            controller.Request.SetConfiguration(new HttpConfiguration());

            return controller;
        }

        [TestMethod]
        public void Get()
        {
            var controller = SetController();

            var result = controller.Get();

            List<GameDTO> resultData;

            result.TryGetContentValue(out resultData);

            Assert.IsNotNull(result);

            Assert.IsNotNull(resultData.Count > 0);
        }

        [TestMethod]
        public void GetAbsoluteRanking()
        {
            var controller = SetController();

            var result = controller.GetAbsoluteRanking(10);

            List<UserTopDTO> resultData;

            result.TryGetContentValue(out resultData);

            Assert.IsNotNull(result);

            Assert.IsNotNull(resultData.Count == 10);
            
            result = controller.GetAbsoluteRanking(100);
            
            result.TryGetContentValue(out resultData);

            Assert.IsNotNull(result);

            Assert.IsNotNull(resultData.Count == 100);
        }

        [TestMethod]
        public void GetRelativeRanking()
        {
            var controller = SetController();

            var result = controller.GetRelativeRanking(10,2);

            List<UserTopDTO> resultData;

            result.TryGetContentValue(out resultData);

            Assert.IsNotNull(result);

            Assert.IsNotNull(resultData.Count == 5);
            
            result = controller.GetRelativeRanking(100, 10);

            result.TryGetContentValue(out resultData);

            Assert.IsNotNull(resultData.Count == 21);

        }

        [TestMethod]
        public void GetById()
        {
            var controller = SetController();

            var result = controller.Get(1);

            GameDTO resultData;

            result.TryGetContentValue(out resultData);

            Assert.IsNotNull(result);
            
            Assert.AreEqual(1, resultData.Id);
            Assert.AreEqual("Game", resultData.Name);
        }

        [TestMethod]
        public void Post()
        {
            var controller = SetController();

            var requestObject = new GameDTO()
            {
                Id = 0,
                Name = "Game_2",
            };

            controller.Validate(requestObject);
            var result = controller.Post(requestObject);
            
            Assert.IsNotNull(result.StatusCode);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

            requestObject = new GameDTO()
            {
                Id = 0
            };

            controller.Validate(requestObject);
            result = controller.Post(requestObject);

            Assert.IsNotNull(result.StatusCode);

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        public void Put()
        {
            var controller = SetController();

            var requestObject = new GameDTO()
            {
                Id = 1,
                Name = "Game_Updated",
            };

            controller.Validate(requestObject);
            var result = controller.Put(requestObject);

            Assert.IsNotNull(result.StatusCode);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

            requestObject = new GameDTO()
            {
                Id = 500,
                Name = "Game_Updated"
            };

            controller.Validate(requestObject);
            result = controller.Put(requestObject);

            Assert.IsNotNull(result.StatusCode);

            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);

            requestObject = new GameDTO()
            {
                Id = 1
            };

            controller.Validate(requestObject);
            result = controller.Put(requestObject);

            Assert.IsNotNull(result.StatusCode);

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);

        }

        [TestMethod]
        public void Delete()
        {
            var controller = SetController();

            var result = controller.Delete(1);

            Assert.IsNotNull(result.StatusCode);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            
            result = controller.Delete(-1);

            Assert.IsNotNull(result.StatusCode);

            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}
