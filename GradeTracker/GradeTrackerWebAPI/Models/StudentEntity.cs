using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GradeTrackerWebAPI.Models
{
    [Table("Students")]
    public class StudentEntity : UserEntity
    {
        // Navigation properties

        public int ClassId { get; set; } // Foreign key reference to Class
        [JsonIgnore]
        public ClassEntity Class { get; set; } = null!;
    }
}
