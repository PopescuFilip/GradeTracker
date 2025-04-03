using GradeTracker.Helpers;
using GradeTracker.Services.Interfaces;
using GradeTracker.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen.Blazor;

namespace GradeTracker.Components.Pages;

public partial class TeacherPage
{
    [Inject]
    public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    [Inject]
    public IGradeHelper GradeHelper { get; set; }

    [Inject]
    public IGradeService GradeService { get; set; }

    [Inject]
    public ISubjectService SubjectService { get; set; }

    private bool IsCreateGradeModalVisible { get; set; }
    private int SubjectId { get; set; }

    private List<GradeViewModel> grades;
    private RadzenDataGrid<GradeViewModel> gradeGrid;

    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var teacherId = int.Parse(authState.User.Identity.Name);
        var subject = await SubjectService.GetSubjectForTeacher(teacherId);
        SubjectId = subject.Id;

        grades = await GradeHelper.GetGradesForSubject(subject.Id);
    }

    private async void HandleGradeCreated()
    {
        grades = await GradeHelper.GetGradesForSubject(SubjectId);

        await gradeGrid.Reload();
        CloseCreateTaskModal();
    }

    private void HandleCreateModelVisibility(bool isVisible)
    {
        IsCreateGradeModalVisible = isVisible;
        StateHasChanged();
    }

    private void CloseCreateTaskModal()
    {
        IsCreateGradeModalVisible = false;
    }

    private void ShowCreateGradeModal()
    {
        IsCreateGradeModalVisible = true;
    }

    private async Task EditRow(GradeViewModel grade)
    {
        await gradeGrid.EditRow(grade);
    }

    private async Task SaveRow(GradeViewModel grade)
    {
        if (grade.Grade < 1 || grade.Grade > 10)
            return;
        await GradeService.UpdateGrade(grade.Id, grade.Grade);
        await gradeGrid.UpdateRow(grade);
    }

    private void CancelEdit(GradeViewModel grade)
    {
        gradeGrid.CancelEditRow(grade);
    }

    private async Task DeleteRow(GradeViewModel grade)
    {
        grades.Remove(grade);
        await GradeService.DeleteGrade(grade.Id);

        await gradeGrid.Reload();
    }
}