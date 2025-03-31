namespace GradeTracker.Models
{
    public class SubjectGradesList
    {
        public string SubjectName { get; set; } = string.Empty;
        public List<GradeEntity> Grades { get; set; } = new List<GradeEntity>();
    }
}
