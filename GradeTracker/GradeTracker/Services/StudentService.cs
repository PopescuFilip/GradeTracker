using GradeTracker.Models;
using GradeTracker.Services.Interfaces;

namespace GradeTracker.Services;

public class StudentService(IConfiguration configuration) : IStudentService
{
    private readonly string _baseUrl = $"{configuration["APIHost"]}/student";
    private readonly HttpClient _httpClient = new();
    public async Task<List<User>> GetStudentsForSubject(int subjectId)
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/get-students-for-subject/{subjectId}");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<User>>();
        }

        return null;
    }
}
