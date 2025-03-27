using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GradeTrackerWebAPI.Models
{
    [Table("Students")]
    public class StudentEntity : UserEntity
    {
        [ForeignKey("Class")]
        public int ClassId { get; set; }
        [JsonIgnore]
        public ClassEntity Class { get; set; } = null!;
    }
}