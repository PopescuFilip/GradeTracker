using GradeTracker.ViewModels;

namespace GradeTracker.Helpers;

public interface IGradeHelper
{
    Task<List<GradeViewModel>> GetGradesForSubject(int subjectId);
}