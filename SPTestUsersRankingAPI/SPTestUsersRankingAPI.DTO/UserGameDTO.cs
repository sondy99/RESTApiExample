
using System.ComponentModel.DataAnnotations;

namespace SPTestUsersRankingAPI.DTO
{
    public class UserGameDTO
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int GameId { get; set; }
    }
}
