using GradeTracker.Models;
using GradeTracker.Services.Interfaces;
using System.Text.Json;
using System.Text;

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

        public async Task<List<GradeEntity>?> GetGradesForSubjectAndStudent(int subjectId, int studentId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/get-grades-for-subject-and-student/{subjectId}/{studentId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<GradeEntity>>();
            }

            return null;
        }

        public async Task<List<GradeEntity>?> GetGradesHistoryForTeacher(int teacherId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/get-grades-history-for-teacher/{teacherId}");

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

        public async Task<bool> UpdateGrade(int gradeId, int newGrade)
        {
            var content = new StringContent(JsonSerializer.Serialize(newGrade), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseUrl}/{gradeId}", content);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteGrade(int gradeId)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{gradeId}");

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ExistsForStudentAndAssignment(int studentId, int assignmentId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/exists-for-student-and-assignment/{studentId}/{assignmentId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<bool>();
            }

            return false;
        }

        public async Task<bool> CreateGrade(CreateGradeRequest createGradeRequest)
        {
            var content = new StringContent(JsonSerializer.Serialize(createGradeRequest), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}", content);

            return response.IsSuccessStatusCode;
        }
    }
}