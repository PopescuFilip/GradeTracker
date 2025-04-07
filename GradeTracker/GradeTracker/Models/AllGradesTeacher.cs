namespace GradeTracker.Models
{
    public class AllGradesTeacher
    {
        public GradeEntity Grade { get; set; } = new GradeEntity();

        public User Student { get; set; } = new User();
    }
}
