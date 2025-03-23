using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GradeTrackerWebAPI.Models
{
    [Table("Subjects")]
    public class SubjectEntity
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        // Navigation properties
        [JsonIgnore]
        public ICollection<AssignmentEntity> Assignments { get; set; } = new List<AssignmentEntity>();

        [JsonIgnore]
        public ICollection<ClassEntity> Classes { get; set; } = new List<ClassEntity>();

        // Teacher relationship
        [JsonIgnore]
        public virtual TeacherEntity Teacher { get; set; } = null!; // Navigation to Teacher
        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }          // Foreign key
    }
}
