using System.Collections.Generic;

namespace SPTestUsersRankingAPI.Model
{
    public class User : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<UserGame> Games;
    }
}
