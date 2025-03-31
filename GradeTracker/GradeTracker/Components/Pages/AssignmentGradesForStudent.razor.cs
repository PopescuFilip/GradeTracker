using GradeTracker.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace GradeTracker.Components.Pages
{
    public partial class AssignmentGradesForStudent
    {
        private List<AssignmentGradesList> AssignmentAndGrades;
        private List<Assignment>? Assignments;
        private bool IsLoading = true;
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
            Assignments = await AssignmentService.GetAssignments() ?? new List<Assignment>();
            foreach (var assignment in Assignments)
            {
                if (assignment != null)
                {
                    var grades = await GradeService.GetGradesForAssignmentAndStudent(StudentId, assignment.Id);
                    if (grades != null)
                    {
                        if (AssignmentAndGrades == null)
                        {
                            AssignmentAndGrades = new List<AssignmentGradesList>();
                        }

                        AssignmentAndGrades.Add(new AssignmentGradesList
                        {
                            AssignmentTitle = assignment.Title,
                            Grades = grades
                        });
                    }
                }

            }
        }
    }
}