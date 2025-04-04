using GradeTracker.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace GradeTracker.Components.Pages
{
    public partial class ViewAllGradesForStudent
    {
        private List<Subject> Subjects { get; set; } = new List<Subject>();
        private List<AllGrades> SubjectAndGrades { get; set; } = new List<AllGrades>();
        private int StudentId { get; set; }
        private bool IsLoading { get; set; } = true;
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
            if (Subjects == null || !Subjects.Any())
            {
                return;
            }

            foreach (var subject in Subjects)
            {
                if (subject != null)
                {
                    var grades = await GradeService.GetGradesForSubjectAndStudent(subject.Id, StudentId);
                    if (grades != null)
                    {
                        if (SubjectAndGrades == null)
                        {
                            SubjectAndGrades = new List<AllGrades>();
                        }
                        foreach (var grade in grades)
                        {
                            SubjectAndGrades.Add(new AllGrades
                            {
                                Subject = subject,
                                Grade = grade
                            });
                        }
                    }
                }
            }
            SubjectAndGrades = SubjectAndGrades
                    .OrderByDescending(x => x.Grade.DateCreated)
                    .ToList();
            StateHasChanged();

        }
        private async Task ToggleSortOrder()
        {
            SortDescending = !SortDescending;

            if (SortDescending)
            {
                SubjectAndGrades = SubjectAndGrades
                    .OrderByDescending(x => x.Grade.DateCreated)
                    .ToList();
            }
            else
            {
                SubjectAndGrades = SubjectAndGrades
                    .OrderBy(x => x.Grade.DateCreated)
                    .ToList();
            }

            StateHasChanged();
        }
    }
}