using GradeTracker.Models;

namespace GradeTracker.Components.Pages;

public partial class SubjectGradesForStudent
{
    private List<SubjectGradesList> SubjectAndGrades { get; set; }
    private List<Subject> Subjects { get; set; }
    private bool IsLoading { get; set; } = true;
    private int StudentId;
    private bool SortDescending = true;

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
                        Grades = SortDescending
                            ? grades.OrderByDescending(x => x.DateCreated).ToList()
                            : grades.OrderBy(x => x.DateCreated).ToList(),
                        AverageSubjectGrades = grades.Select(x => x.Grade).DefaultIfEmpty(0).Average()
                    });
                }
            }
        }
        StateHasChanged();

    }

    private async Task ToggleSortOrder()
    {
        SortDescending = !SortDescending;

        foreach (var subjectGrades in SubjectAndGrades)
        {
            if (SortDescending)
            {
                subjectGrades.Grades = subjectGrades.Grades
                    .OrderByDescending(x => x.DateCreated)
                    .ToList();
            }
            else
            {
                subjectGrades.Grades = subjectGrades.Grades
                    .OrderBy(x => x.DateCreated)
                    .ToList();
            }
        }

        StateHasChanged();
    }

}