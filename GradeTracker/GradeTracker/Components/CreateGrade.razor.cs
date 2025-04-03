using GradeTracker.Models;
using GradeTracker.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace GradeTracker.Components;

public partial class CreateGrade
{
    [Inject]
    public IStudentService StudentService{ get; set; }

    [Inject]
    public IAssignmentService AssignmentService{ get; set; }

    [Inject]
    public IGradeService GradeService { get; set; }

    [Parameter]
    public int SubjectId { get; set; }

    [Parameter]
    public EventCallback OnGradeCreated { get; set; }

    [Parameter]
    public EventCallback<bool> OnVisibilityChanged { get; set; }

    [Parameter]
    public bool IsVisible
    {
        get => _isVisible;
        set
        {
            if (value)
                OnShow();
            _isVisible = value;
        }
    }

    private bool _isVisible;
    private bool showErrorMessage;
    private List<StudentViewModel> students;
    private List<AssignmentViewModel> assignments;

    private GradeEntity GradeBeingCreated { get; set; } = new GradeEntity();

    protected override async Task OnInitializedAsync()
    {
        await OnShow();
    }

    private async Task HandleSubmit()
    {
        if (GradeBeingCreated.Grade < 1 || GradeBeingCreated.Grade > 10)
        {
            showErrorMessage = true;
            return;
        }

        var createGradeRequest = new CreateGradeRequest(
            GradeBeingCreated.Grade,
            GradeBeingCreated.StudentId,
            GradeBeingCreated.AssignmentId);

        var result = await GradeService.CreateGrade(createGradeRequest);
        if (!result)
        {
            showErrorMessage = true;
            return;
        }

        showErrorMessage = false;
        GradeBeingCreated = new GradeEntity();
        OnGradeCreated.InvokeAsync();
    }

    private void CloseCreateTaskModal()
    {
        IsVisible = false;
        OnVisibilityChanged.InvokeAsync(false);
    }

    private async Task OnShow()
    {
        students = (await StudentService.GetStudentsForSubject(SubjectId))
            .Select(s => new StudentViewModel(s.Id, s.FirstName, s.LastName))
            .ToList();

        assignments = (await AssignmentService.GetAssignments())
            .Where(a => a.SubjectId == SubjectId)
            .Select(a => new AssignmentViewModel(a.Id, a.Title))
            .ToList();

        StateHasChanged();
    }

    private void OnSelectStudent(object student)
    {
        GradeBeingCreated.StudentId = ((StudentViewModel)student).Id;
    }

    private async void OnSelectAssignment(object assignment)
    {
        GradeBeingCreated.AssignmentId = ((AssignmentViewModel)assignment).Id;
    }
}

public record StudentViewModel(int Id, string FirstName, string LastName)
{
    public override string ToString() => $"{FirstName} {LastName}";
}

public record AssignmentViewModel(int Id, string Title)
{
    public override string ToString() => Title;
}