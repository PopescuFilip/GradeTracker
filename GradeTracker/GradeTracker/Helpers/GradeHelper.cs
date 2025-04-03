using GradeTracker.Services.Interfaces;
using GradeTracker.ViewModels;

namespace GradeTracker.Helpers;

public class GradeHelper(IGradeService gradeService, IStudentService studentService) : IGradeHelper
{
    private readonly IGradeService _gradeService = gradeService;
    private readonly IStudentService _studentService = studentService;

    public async Task<List<GradeViewModel>> GetGradesForSubject(int subjectId)
    {
        var grades = new List<GradeViewModel>();
        var students = await _studentService.GetStudentsForSubject(subjectId);
        foreach (var student in students)
        {
            var gradesForStudent = await _gradeService.GetGradesForSubjectAndStudent(subjectId, student.Id);
            grades.AddRange(gradesForStudent, student);
        }

        return grades;
    }
}