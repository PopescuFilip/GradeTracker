using GradeTracker.Models;
using GradeTracker.Services.Interfaces;

namespace GradeTracker.Services
{
    public class SubjectService(IConfiguration configuration) : ISubjectService
    {
        private readonly string _baseUrl = $"{configuration["APIHost"]}/subject";
        private readonly HttpClient _httpClient = new();
        public async Task<List<Subject>?> GetAllSubjects()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/get-all");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Subject>>();
            }

            return null;
        }

        public async Task<List<Subject>?> GetSubjectsForStudent(int studentId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/get-subjects-for-student{studentId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Subject>>();
            }

            return null;
        }
    }
}
