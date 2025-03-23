using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GradeTrackerWebAPI.Models
{
    [Table("Assignments")]
    public class AssignmentEntity
    {
        public int Id { get; set; }
        [Range(0, 10, ErrorMessage = "Grade must be between 0 and 10.")]
        public int Grade { get; set; }
        [Required, MaxLength(50)]
        public string Title { get; set; } = string.Empty;
        [Required, MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-set on creation
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public bool IsGraded { get; set; }

        // Navigation properties

        [JsonIgnore]
        public SubjectEntity Subject { get; set; } = null!;
        [ForeignKey("Subjects")]
        public int SubjectId { get; set; }
    }
}
