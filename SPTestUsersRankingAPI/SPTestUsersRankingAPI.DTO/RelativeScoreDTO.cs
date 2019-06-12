
using System.ComponentModel.DataAnnotations;

namespace SPTestUsersRankingAPI.DTO
{
    public class RelativeScoreDTO
    {
        [Required]
        public int User { get; set; }

        [Required]
        public string Score { get; set; } //this could be int, but in the example you want me to use +10 / -20
    }
}
