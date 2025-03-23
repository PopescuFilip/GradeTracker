using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GradeTrackerWebAPI.Models
{
    [Table("Teachers")]
    public class TeacherEntity : UserEntity
    {
        // Navigation properties

        [JsonIgnore]
        public SubjectEntity Subject { get; set; } = null!;
    }
}
