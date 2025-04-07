using GradeTracker.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace GradeTracker.Components.Pages
{
    public partial class ViewAllGradesForStudent
    {
        private List<Subject> Subjects { get; set; } = new List<Subject>();
        private List<AllGradesStudent> SubjectAndGrades { get; set; } = new List<AllGradesStudent>();
        private int StudentId { get; set; }
        private bool IsLoading { get; set; } = true;
        private bool SortDescending = true;

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            if (authState.User.Identity != null && !string.IsNullOrEmpty(authState.User.Identity.Name))
            {
                StudentId = int.Parse(authState.User.Identity.Name);
            }
            else
            {
                StudentId = 0;
            }
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
                            SubjectAndGrades = new List<AllGradesStudent>();
                        }
                        foreach (var grade in grades)
                        {
                            SubjectAndGrades.Add(new AllGradesStudent
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
        private void ToggleSortOrder()
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