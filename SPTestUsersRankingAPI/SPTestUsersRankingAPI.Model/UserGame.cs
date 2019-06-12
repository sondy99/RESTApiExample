
namespace SPTestUsersRankingAPI.Model
{
    public class UserGame : EntityBase
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }

        public int Score { get; set; }
    }
}
