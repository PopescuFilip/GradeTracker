namespace GradeTracker.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int SubjectId { get; set; }
    }
}
