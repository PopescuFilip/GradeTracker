using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GradeTrackerWebAPI.Models
{
    [Table("Assignments")]
    public class AssignmentEntity : Entity
    {
        [Required, MaxLength(50)]
        public string Title { get; set; } = string.Empty;
        [Required, MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        [ForeignKey("Subject")]
        public int SubjectId { get; set; }

        [JsonIgnore]
        public SubjectEntity Subject { get; set; } = null!;

        [JsonIgnore]
        public ICollection<GradeEntity> Grades { get; set; } = [];
    }
}