
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
    public class UserControllerTest
    {
        private UserController SetController()
        {
            UserController controller = new UserController();
            controller.Request = new HttpRequestMessage();
            controller.Request.SetConfiguration(new HttpConfiguration());

            return controller;
        }

        [TestMethod]
        public void Get()
        {
            var controller = SetController();

            var result = controller.Get();

            List<UserDTO> resultData;

            result.TryGetContentValue(out resultData);

            Assert.IsNotNull(result);

            Assert.IsNotNull(resultData.Count > 1);

            Assert.AreEqual(1, resultData.First().Id);
            Assert.AreEqual("FirstName_1", resultData.First().FirstName);
            Assert.AreEqual("LastName_1", resultData.First().LastName);
        }

        [TestMethod]
        public void GetById()
        {
            var controller = SetController();

            var result = controller.Get(2);

            UserDTO resultData;

            result.TryGetContentValue(out resultData);

            Assert.IsNotNull(result);
            
            Assert.AreEqual(2, resultData.Id);
            Assert.AreEqual("FirstName_2", resultData.FirstName);
            Assert.AreEqual("LastName_2", resultData.LastName);
        }

        [TestMethod]
        public void Post()
        {
            var controller = SetController();

            var requestObject = new UserDTO()
            {
                Id = 0,
                FirstName = "Daniel",
                LastName = "Cardoza"
            };

            controller.Validate(requestObject);
            var result = controller.Post(requestObject);
            
            Assert.IsNotNull(result.StatusCode);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

            requestObject = new UserDTO()
            {
                Id = 0,
                FirstName = "Daniel"
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

            var requestObject = new UserDTO()
            {
                Id = 5,
                FirstName = "Daniel",
                LastName = "Cardoza"
            };

            controller.Validate(requestObject);
            var result = controller.Put(requestObject);

            Assert.IsNotNull(result.StatusCode);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

            requestObject = new UserDTO()
            {
                Id = 500,
                FirstName = "Daniel",
                LastName = "Cardoza"
            };

            controller.Validate(requestObject);
            result = controller.Put(requestObject);

            Assert.IsNotNull(result.StatusCode);

            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);

            requestObject = new UserDTO()
            {
                Id = 5,
                FirstName = "Daniel"
            };

            controller.Validate(requestObject);
            result = controller.Put(requestObject);

            Assert.IsNotNull(result.StatusCode);

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);

        }

        [TestMethod]
        public void PutSubscribeUserToGame()
        {
            var controller = SetController();

            var requestObject = new UserGameDTO()
            {
                GameId = 1,
                UserId = 1,
            };

            controller.Validate(requestObject);
            var result = controller.PutSubscribeUserToGame(requestObject);

            Assert.IsNotNull(result.StatusCode);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

            requestObject = new UserGameDTO()
            {
                GameId = 1,
                UserId = 500,
            };

            controller.Validate(requestObject);
            result = controller.PutSubscribeUserToGame(requestObject);

            Assert.IsNotNull(result.StatusCode);

            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod]
        public void PutSubmitAbsoluteScore()
        {
            var controller = SetController();

            var requestObject = new AbsoluteScoreDTO()
            {
                Total = 100,
                User = 1,
            };

            controller.Validate(requestObject);
            var result = controller.PutSubmitAbsoluteScore(requestObject);

            Assert.IsNotNull(result.StatusCode);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

            requestObject = new AbsoluteScoreDTO()
            {
                Total = 100,
                User = 500,
            };

            controller.Validate(requestObject);
            result = controller.PutSubmitAbsoluteScore(requestObject);

            Assert.IsNotNull(result.StatusCode);

            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);

            requestObject = new AbsoluteScoreDTO()
            {
                Total = 915154,
                User = 1,
            };

            controller.Validate(requestObject);
            result = controller.PutSubmitAbsoluteScore(requestObject);

            Assert.IsNotNull(result.StatusCode);

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        public void PutSubmitRelativeScore()
        {
            var controller = SetController();

            var requestObject = new RelativeScoreDTO()
            {
                Score = "+10",
                User = 1,
            };

            controller.Validate(requestObject);
            var result = controller.PutSubmitRelativeScore(requestObject);

            Assert.IsNotNull(result.StatusCode);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

            requestObject = new RelativeScoreDTO()
            {
                Score = "-150",
                User = 1,
            };

            controller.Validate(requestObject);
            result = controller.PutSubmitRelativeScore(requestObject);

            Assert.IsNotNull(result.StatusCode);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

            requestObject = new RelativeScoreDTO()
            {
                Score = "+50",
                User = 500,
            };

            controller.Validate(requestObject);
            result = controller.PutSubmitRelativeScore(requestObject);

            Assert.IsNotNull(result.StatusCode);

            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);

            requestObject = new RelativeScoreDTO()
            {
                Score = "carlos",
                User = 1,
            };

            controller.Validate(requestObject);
            result = controller.PutSubmitRelativeScore(requestObject);

            Assert.IsNotNull(result.StatusCode);

            Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);
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
