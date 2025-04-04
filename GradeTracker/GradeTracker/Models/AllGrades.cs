namespace GradeTracker.Models
{
    public class AllGrades
    {
        public Subject Subject { get; set; } = new Subject();
        public GradeEntity Grade { get; set; } = new GradeEntity();
    }
}
