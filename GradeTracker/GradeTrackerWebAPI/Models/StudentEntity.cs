using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GradeTrackerWebAPI.Models
{
    [Table("Students")]
    public class StudentEntity : UserEntity
    {
        [JsonIgnore]
        public ICollection<SubjectEntity> Subjects { get; set; } = [];

        [JsonIgnore]
        public ICollection<GradeEntity> Grades { get; set; } = [];
    }
}