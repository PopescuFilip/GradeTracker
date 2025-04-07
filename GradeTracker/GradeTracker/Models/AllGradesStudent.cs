namespace GradeTracker.Models
{
    public class AllGradesStudent
    {
        public Subject Subject { get; set; } = new Subject();
        public GradeEntity Grade { get; set; } = new GradeEntity();
    }
}
