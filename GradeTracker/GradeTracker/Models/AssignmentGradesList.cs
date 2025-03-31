namespace GradeTracker.Models
{
    public class AssignmentGradesList
    {
        public string AssignmentTitle { get; set; } = string.Empty;
        public List<GradeEntity> Grades { get; set; } = new List<GradeEntity>();
    }
}
