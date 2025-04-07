using GradeTracker.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace GradeTracker.Components;

public partial class CreateGrade
{
    [Inject]
    public IStudentService StudentService { get; set; }

    [Inject]
    public IAssignmentService AssignmentService { get; set; }

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

    private List<int> SelectedStudents { get; set; } = [];
    private int SelectedAssignment { get; set; }
    private int GradeValue { get; set; } = 1;

    protected override async Task OnInitializedAsync()
    {
        await OnShow();
    }

    private async Task HandleSubmit()
    {
        if (GradeValue < 1 || GradeValue > 10 || SelectedStudents.Count == 0 || SelectedAssignment == -1)
        {
            showErrorMessage = true;
            return;
        }

        bool allSuccess = true;
        foreach (var studentId in SelectedStudents)
        {
            var createGradeRequest = new CreateGradeRequest(
                GradeValue,
                studentId,
                SelectedAssignment
            );

            var result = await GradeService.CreateGrade(createGradeRequest);
            if (!result)
            {
                allSuccess = false;
            }
        }

        showErrorMessage = !allSuccess;
        if (allSuccess)
        {
            SelectedStudents.Clear();
            SelectedAssignment = -1;
            GradeValue = 1;
            await OnGradeCreated.InvokeAsync();
        }
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

    private void OnSelectAssignment(object assignment)
    {
        SelectedAssignment = ((AssignmentViewModel)assignment).Id;
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