using GradeTracker.Models;
using GradeTracker.Services.Interfaces;

namespace GradeTracker.Services;

public class StudentService(IConfiguration configuration) : IStudentService
{
    private readonly string _baseUrl = $"{configuration["APIHost"]}/student";
    private readonly HttpClient _httpClient = new();

    /// <summary>
    /// Adds a subject to a student.
    /// </summary>
    /// <param name="userId">The unique identifier of the student.</param>
    /// <param name="subjectId">The unique identifier of the subject to add.</param>
    /// <returns>
    /// <c>true</c> if the subject was successfully added; otherwise, <c>false</c>.
    /// </returns>
    public async Task<bool> AddSubject(int userId, int subjectId)
    {
        // Send a POST request to add the subject.
        var response = await _httpClient.PostAsync($"{_baseUrl}/add-subject/{userId}/{subjectId}", null);
        return response.IsSuccessStatusCode;
    }

    /// <summary>
    /// Retrieves all students enrolled in a given subject.
    /// </summary>
    /// <param name="subjectId">The unique identifier of the subject.</param>
    /// <returns>A list of students enrolled in the specified subject, or <c>null</c> if the request fails.</returns>
    public async Task<List<User>> GetStudentsForSubject(int subjectId)
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/get-students-for-subject/{subjectId}");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<User>>();
        }
        return null;
    }

    /// <summary>
    /// Removes a subject from a student's list of subjects.
    /// </summary>
    /// <param name="userId">The unique identifier of the student.</param>
    /// <param name="subjectId">The unique identifier of the subject to remove.</param>
    /// <returns>
    /// <c>true</c> if the subject was successfully removed; otherwise, <c>false</c>.
    /// </returns>
    public async Task<bool> RemoveSubject(int userId, int subjectId)
    {
        // Send a POST request to remove the subject.
        var response = await _httpClient.PostAsync($"{_baseUrl}/remove-subject/{userId}/{subjectId}", null);
        return response.IsSuccessStatusCode;
    }
}
