using GradeTracker.Models;

namespace GradeTracker.ViewModels;

public class GradeViewModel(GradeEntity gradeEntity, User student)
{
    private readonly int _studentId = gradeEntity.StudentId;
    private readonly int _assignmentId = gradeEntity.AssignmentId;

    public int Id { get; } = gradeEntity.Id;
    public int Grade { get; set; } = gradeEntity.Grade;
    public DateTime DateCreated { get; } = gradeEntity.DateCreated;
    public string StudentFullName { get; } = $"{student.FirstName} {student.LastName}";

    private GradeEntity ToGradeEntity()
    {
        return new GradeEntity()
        {
            Id = Id,
            StudentId = _studentId,
            AssignmentId = _assignmentId,
            DateCreated = DateCreated,
            Grade = Grade,
        };
    }
}