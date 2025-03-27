using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GradeTrackerWebAPI.Models
{
    [Table("Users")]
    public class UserEntity : Entity
    {
        [Required, MaxLength(50)]
        public string Username { get; set; } = string.Empty;
        [Required, MaxLength(50)]
        public string Password { get; set; } = string.Empty;
        [Required, MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;
        [Required, MaxLength(50)]
        public string LastName { get; set; } = string.Empty;
    }
}
