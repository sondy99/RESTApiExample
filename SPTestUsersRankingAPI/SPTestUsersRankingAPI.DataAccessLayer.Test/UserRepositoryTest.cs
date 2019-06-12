using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPTestUsersRankingAPI.Model;

namespace SPTestUsersRankingAPI.DataAccessLayer.Test
{
    [TestClass]
    public class UserRepositoryTest
    {
        [TestMethod]
        public void GetAll()
        {
            var userRepository = new UserRepository(Database.Context.Instance);

            var result = userRepository.GetAll();

            Assert.IsNotNull(result);

            Assert.IsNotNull(result.Count > 1);
        }

        [TestMethod]
        public void GetById()
        {
            var userRepository = new UserRepository(Database.Context.Instance);

            var result = userRepository.GetById(2);

            Assert.IsNotNull(result);

            Assert.AreEqual(2, result.Id);
            Assert.AreEqual("FirstName_2", result.FirstName);
            Assert.AreEqual("LastName_2", result.LastName);
        }

        [TestMethod]
        public void Create()
        {
            var userRepository = new UserRepository(Database.Context.Instance);

            var requestObject = new User()
            {
                Id = 0,
                FirstName = "Daniel",
                LastName = "Cardoza"
            };

            userRepository.Create(requestObject);
        }

        [TestMethod]
        public void Update()
        {
            var userRepository = new UserRepository(Database.Context.Instance);

            var requestObject = new User()
            {
                Id = 5,
                FirstName = "Daniel",
                LastName = "Cardoza"
            };

            var result = userRepository.Update(requestObject);

            Assert.IsNotNull(result);

            Assert.AreEqual(5, result.Id);
            Assert.AreEqual("Daniel", result.FirstName);
            Assert.AreEqual("Cardoza", result.LastName);

            requestObject = new User()
            {
                Id = 500,
                FirstName = "Daniel",
                LastName = "Cardoza"
            };

            result = userRepository.Update(requestObject);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Delete()
        {
            var userRepository = new UserRepository(Database.Context.Instance);

            var result = userRepository.Delete(new User() { Id = 100});

            Assert.IsNotNull(result);

            result = userRepository.Delete(new User() { Id = -1 });

            Assert.IsNull(result);
        }
    }
}
