namespace GradeTracker.Models
{
    public class GradeEntity
    {
        public int Id { get; set; }
        public int Grade { get; set; }
        public DateTime DateCreated { get; set; }

        public int StudentId { get; set; }

        public int AssignmentId { get; set; }
    }
}
