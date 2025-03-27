using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GradeTrackerWebAPI.Models;

[Table("Grades")]
public class GradeEntity : Entity
{
    [Range(0, 10, ErrorMessage = "Grade must be between 0 and 10.")]
    public int Grade { get; set; }

    public bool IsGraded { get; set; }

    [ForeignKey("Student")]
    public int StudentId { get; set; }

    [JsonIgnore]
    public StudentEntity Student { get; set; } = null!;

    [ForeignKey("Assignment")]
    public int AssignmentId { get; set; }

    [JsonIgnore]
    public AssignmentEntity Assignment { get; set; } = null!;
}
