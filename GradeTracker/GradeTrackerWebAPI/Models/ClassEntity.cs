using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GradeTrackerWebAPI.Models
{
    [Table("Classes")]
    public class ClassEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<StudentEntity> Students { get; set; } = new List<StudentEntity>();

        [JsonIgnore]
        public ICollection<SubjectEntity> Subjects { get; set; } = new List<SubjectEntity>();
    }
}
