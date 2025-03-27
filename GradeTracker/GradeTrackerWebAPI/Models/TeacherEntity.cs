using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GradeTrackerWebAPI.Models
{
    [Table("Teachers")]
    public class TeacherEntity : UserEntity
    {
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        [JsonIgnore]
        public SubjectEntity Subject { get; set; } = null!;
    }
}