using GradeTracker.Models;
using GradeTracker.ViewModels;

namespace GradeTracker.Helpers;

public static class Extensions
{
    public static void AddRange(this List<GradeViewModel> gradeViewModels, IEnumerable<GradeEntity> grades, User student)
    {
        foreach (var grade in grades)
            gradeViewModels.Add(new(grade, student));
    }
}
