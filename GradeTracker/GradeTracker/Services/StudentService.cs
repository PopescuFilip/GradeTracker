using GradeTracker.Models;
using GradeTracker.Services.Interfaces;

namespace GradeTracker.Services;

public class StudentService(IConfiguration configuration) : IStudentService
{
    private readonly string _baseUrl = $"{configuration["APIHost"]}/student";
    private readonly HttpClient _httpClient = new();

    public Task<bool> AddSubject(int userId, int subjectId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<User>> GetStudentsForSubject(int subjectId)
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/get-students-for-subject/{subjectId}");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<User>>();
        }

        return null;
    }

    public Task<bool> RemoveSubject(int userId, int subjectId)
    {
        throw new NotImplementedException();
    }
}
