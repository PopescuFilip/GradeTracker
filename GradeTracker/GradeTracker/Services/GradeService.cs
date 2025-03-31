using GradeTracker.Models;
using GradeTracker.Services.Interfaces;

namespace GradeTracker.Services
{
    public class GradeService(IConfiguration configuration) : IGradeService
    {
        private readonly string _baseUrl = $"{configuration["APIHost"]}/grade";
        private readonly HttpClient _httpClient = new();

        public async Task<List<GradeEntity>?> GetGradesForStudent(int studentId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/get-all-grades/{studentId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<GradeEntity>>();
            }

            return null;
        }

        public async Task<List<GradeEntity>> GetGradesForSubjectAndStudent(int subjectId, int studentId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/get-grades-for-subject-and-student/{subjectId}/{studentId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<GradeEntity>>();
            }

            return null;
        }

        public async Task<List<GradeEntity>?> GetGradesForAssignmentAndStudent(int studentId, int assignmentId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/get-grades-for-assignment/{studentId}/{assignmentId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<GradeEntity>>();
            }

            return null;
        }
    }
}
