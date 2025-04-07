using GradeTracker.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace GradeTracker.Components.Pages
{
    public partial class ViewAllGradesForTeacher
    {
        private List<Subject> Subjects { get; set; } = new List<Subject>();
        private List<AllGradesTeacher> UserAndGrades { get; set; } = new List<AllGradesTeacher>();
        private int TeacherId { get; set; }
        private bool IsLoading { get; set; } = true;
        private bool SortDescending = true;

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            TeacherId = int.Parse(authState.User.Identity.Name);
            await LoadGrades();

            IsLoading = false;
        }

        private async Task LoadGrades()
        {
                    var grades = await GradeService.GetGradesHistoryForTeacher(TeacherId);
            if (grades != null)
            {
                foreach (var grade in grades)
                {
                    var student = await UserService.GetUserById(grade.StudentId);
                    if (student != null)
                    {
                        UserAndGrades.Add(new AllGradesTeacher
                        {
                            Student = student,
                            Grade = grade
                        });
                    }
                }
            }

            UserAndGrades = UserAndGrades
                    .OrderByDescending(x => x.Grade.DateCreated)
                    .ToList();
            StateHasChanged();
        }
        private async Task ToggleSortOrder()
        {
            SortDescending = !SortDescending;

            if (SortDescending)
            {
                UserAndGrades = UserAndGrades
                    .OrderByDescending(x => x.Grade.DateCreated)
                    .ToList();
            }
            else
            {
                UserAndGrades = UserAndGrades
                    .OrderBy(x => x.Grade.DateCreated)
                    .ToList();
            }

            StateHasChanged();
        }
    }
}