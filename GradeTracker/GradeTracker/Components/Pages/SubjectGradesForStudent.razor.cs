using GradeTracker.Models;

namespace GradeTracker.Components.Pages;

public partial class SubjectGradesForStudent
{
    private List<SubjectGradesList> SubjectAndGrades { get; set; }
    private List<Subject> Subjects { get; set; }
    private bool IsLoading { get; set; } = true;
    private int StudentId;

    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        StudentId = int.Parse(authState.User.Identity.Name);
        await LoadGrades();

        IsLoading = false;
    }

    private async Task LoadGrades()
    {
        Subjects = await SubjectService.GetAllSubjects() ?? new List<Subject>();
        foreach (var subject in Subjects)
        {
            if (subject != null)
            {
                var grades = await GradeService.GetGradesForSubjectAndStudent(subject.Id, StudentId);
                if (grades != null)
                {
                    if (SubjectAndGrades == null)
                    {
                        SubjectAndGrades = new List<SubjectGradesList>();
                    }
                    SubjectAndGrades.Add(new SubjectGradesList
                    {
                        SubjectName = subject.Name,
                        Grades = grades
                    });
                }
            }

        }
    }

}