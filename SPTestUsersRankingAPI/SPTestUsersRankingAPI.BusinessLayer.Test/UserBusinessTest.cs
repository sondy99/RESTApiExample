using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPTestUsersRankingAPI.DTO;

namespace SPTestUsersRankingAPI.BusinessLayer.Test
{
    [TestClass]
    public class UserBusinessTest
    {
        [TestMethod]
        public void GetAll()
        {
            var userBusiness = new UserBusiness();

            var result = userBusiness.GetAll();
            
            Assert.IsNotNull(result);

            Assert.IsNotNull(result.Count > 1);
        }

        [TestMethod]
        public void GetById()
        {
            var userBusiness = new UserBusiness();

            var result = userBusiness.GetById(2);
            
            Assert.IsNotNull(result);

            Assert.AreEqual(2, result.Id);
            Assert.AreEqual("FirstName_2", result.FirstName);
            Assert.AreEqual("LastName_2", result.LastName);
        }

        [TestMethod]
        public void Create()
        {
            var userBusiness = new UserBusiness();

            var requestObject = new UserDTO()
            {
                Id = 0,
                FirstName = "Daniel",
                LastName = "Cardoza"
            };
            
            userBusiness.Create(requestObject);
        }

        [TestMethod]
        public void Update()
        {
            var userBusiness = new UserBusiness();

            var requestObject = new UserDTO()
            {
                Id = 5,
                FirstName = "Daniel",
                LastName = "Cardoza"
            };
            
            var result = userBusiness.Update(requestObject);

            Assert.IsNotNull(result);

            Assert.AreEqual(5, result.Id);
            Assert.AreEqual("Daniel", result.FirstName);
            Assert.AreEqual("Cardoza", result.LastName);

            requestObject = new UserDTO()
            {
                Id = 500,
                FirstName = "Daniel",
                LastName = "Cardoza"
            };
            
            result = userBusiness.Update(requestObject);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void SubscribeUserToGame()
        {
            var userBusiness = new UserBusiness();

            var requestObject = new UserGameDTO()
            {
                GameId = 1,
                UserId = 1,
            };
            
            var result = userBusiness.SubscribeUserToGame(requestObject);

            Assert.IsNotNull(result);
            
            requestObject = new UserGameDTO()
            {
                GameId = 1,
                UserId = 500,
            };
            
            result = userBusiness.SubscribeUserToGame(requestObject);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void SubmitAbsoluteScore()
        {
            var userBusiness = new UserBusiness();

            var requestObject = new AbsoluteScoreDTO()
            {
                Total = 100,
                User = 1,
            };
            
            var result = userBusiness.SubmitAbsoluteScore(requestObject);

            Assert.IsNotNull(result);
            
            requestObject = new AbsoluteScoreDTO()
            {
                Total = 100,
                User = 500,
            };
            
            result = userBusiness.SubmitAbsoluteScore(requestObject);
            
            Assert.IsNull(result);
            
            requestObject = new AbsoluteScoreDTO()
            {
                Total = 915154,
                User = 1,
            };

            result = userBusiness.SubmitAbsoluteScore(requestObject);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void SubmitRelativeScore()
        {
            var userBusiness = new UserBusiness();

            var requestObject = new RelativeScoreDTO()
            {
                Score = "+10",
                User = 1,
            };
            
            var result = userBusiness.SubmitRelativeScore(requestObject);

            Assert.IsNotNull(result);
            
            requestObject = new RelativeScoreDTO()
            {
                Score = "-150",
                User = 1,
            };

            result = userBusiness.SubmitRelativeScore(requestObject);

            Assert.IsNotNull(result);
            
            requestObject = new RelativeScoreDTO()
            {
                Score = "+50",
                User = 500,
            };
            
            result = userBusiness.SubmitRelativeScore(requestObject);

            Assert.IsNull(result);
            
            requestObject = new RelativeScoreDTO()
            {
                Score = "carlos",
                User = 1,
            };
            
            try
            {
                result = userBusiness.SubmitRelativeScore(requestObject);
                Assert.Fail(); // If it gets to this line, no exception was thrown
            }
            catch (Exception e)
            {
            }
        }

        [TestMethod]
        public void Delete()
        {
            var userBusiness = new UserBusiness();

            var result = userBusiness.Delete(100);

            Assert.IsNotNull(result);
            
            result = userBusiness.Delete(-1);

            Assert.IsNull(result);
        }
    }
}
