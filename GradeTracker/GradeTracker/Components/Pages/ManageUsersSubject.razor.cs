using GradeTracker.Models;
using GradeTracker.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace GradeTracker.Components.Pages
{
    public partial class ManageUsersSubject
    {

        [Inject]
        private NavigationManager Navigation { get; set; }

        private int SubjectId { get; set; }

        private String SubjectName { get; set; } = string.Empty;
        private int NewStudentId { get; set; }
        private List<User> StudentsInSubject { get; set; } = new();
        private string Message { get; set; } = string.Empty;
        private bool IsLoading { get; set; } = true;
        private bool IsError { get; set; }
        private bool HasSubject { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var teacherId = int.Parse(authState.User.Identity.Name);

            var subject = await SubjectService.GetSubjectForTeacher(teacherId);

            if (subject == null)
            {
                HasSubject = false;
            }
            else
            {
                HasSubject = true;
                SubjectId = subject.Id;
                SubjectName = subject.Name;
                await LoadStudents();
            }

            IsLoading = false;
        }

        private async Task LoadStudents()
        {
            var students = await StudentService.GetStudentsForSubject(SubjectId);
            StudentsInSubject = students ?? new List<User>();
            StateHasChanged();
        }

        private async Task AddStudent()
        {
            if (NewStudentId <= 0)
            {
                ShowMessage("Please enter a valid Student ID", true);
                return;
            }

            var result = await StudentService.AddSubject(NewStudentId, SubjectId);
            if (result)
            {
                ShowMessage("Student added successfully");
                await LoadStudents();
                NewStudentId = 0;
            }
            else
            {
                ShowMessage("Failed to add student. Check if the student ID is valid.", true);
            }
        }

        private async Task RemoveStudent(int studentId)
        {
            var result = await StudentService.RemoveSubject(studentId, SubjectId);
            if (result)
            {
                ShowMessage("Student removed successfully");
                await LoadStudents();
            }
            else
            {
                ShowMessage("Failed to remove student", true);
            }
        }

        private void ShowMessage(string message, bool isError = false)
        {
            Message = message;
            IsError = isError;
            StateHasChanged();
        }
    }
}