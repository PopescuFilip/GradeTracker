using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GradeTrackerWebAPI.Models
{
    [Table("Classes")]
    public class ClassEntity
    {
        public int Id { get; set; }

        // Navigation properties (no primitive lists like SubjectsIds/StudentsIds)
        [JsonIgnore]
        public ICollection<StudentEntity> Students { get; set; } = new List<StudentEntity>();

        [JsonIgnore]
        public ICollection<ClassSubject> ClassSubjects { get; set; } = new List<ClassSubject>();
    }

    // Join entity for Class ↔ Subject (many-to-many)
    [Table("ClassSubjects")]
    public class ClassSubject
    {
        public int ClassId { get; set; }
        public ClassEntity Class { get; set; } = null!;

        public int SubjectId { get; set; }
        public SubjectEntity Subject { get; set; } = null!;
    }
}
