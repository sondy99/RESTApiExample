
using System.ComponentModel.DataAnnotations;

namespace SPTestUsersRankingAPI.DTO
{
    public class GameDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
