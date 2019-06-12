
using System.ComponentModel.DataAnnotations;

namespace SPTestUsersRankingAPI.DTO
{
    public class UserDTO
    {
        [Required]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }
    }
}
