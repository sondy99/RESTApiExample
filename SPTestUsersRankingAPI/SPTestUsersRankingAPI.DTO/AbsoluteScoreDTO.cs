
using System.ComponentModel.DataAnnotations;

namespace SPTestUsersRankingAPI.DTO
{
    public class AbsoluteScoreDTO
    {
        [Required]
        public int User { get; set; }

        [Required]
        [Range(0, 1000)]
        public int Total { get; set; }
    }
}
