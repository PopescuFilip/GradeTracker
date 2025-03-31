using GradeTracker.Models;
using GradeTracker.Services.Interfaces;

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
    }
}
