using GradeTracker.Models;
using GradeTracker.Services.Interfaces;
using System.Text.Json;
using System.Text;

namespace GradeTracker.Services
{
    public class AssignmentService(IConfiguration configuration) : IAssignmentService
    {
        private readonly string _baseUrl = $"{configuration["APIHost"]}/assignment";
        private readonly HttpClient _httpClient = new();

        public async Task<List<Assignment>?> GetAssignments()
        {

            var response = await _httpClient.GetAsync($"{_baseUrl}/get-all");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Assignment>>();
            }

            return null;
        }

        public async Task<bool> CreateAssignment(CreateAssignmentRequest createAssignmentRequest)
        {
            var content = new StringContent(JsonSerializer.Serialize(createAssignmentRequest), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}", content);

            return response.IsSuccessStatusCode;
        }
    }
}