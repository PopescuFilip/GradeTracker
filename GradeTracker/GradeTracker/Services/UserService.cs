using GradeTracker.Enums;
using GradeTracker.Models;
using GradeTracker.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace GradeTracker.Services;

public class UserService(IConfiguration configuration) : IUserService
{
    private readonly string _baseUrl = $"{configuration["APIHost"]}/user";
    private readonly HttpClient _httpClient = new();

    public async Task<User?> Login(string username, string password)
    {
        var loginRequest = new LoginRequest(username, password);

        var jsonContent = new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"{_baseUrl}/login", jsonContent);

        if (response.IsSuccessStatusCode)
        {
            var user = await response.Content.ReadFromJsonAsync<User>();
            return user;
        }

        return null;
    }

    public async Task<bool> ResetPassword(string username, string newPassword)
    {
        var resetPasswordRequest = new ResetPasswordRequest(username, newPassword);

        var jsonContent = new StringContent(JsonSerializer.Serialize(resetPasswordRequest), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"{_baseUrl}/reset-password", jsonContent);

        return response.IsSuccessStatusCode;
    }

    public async Task<UserType> GetUserType(int id)
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/user-type/{id}");

        if (response.IsSuccessStatusCode)
        {
            var userType = await response.Content.ReadFromJsonAsync<UserType>();
            return userType;
        }

        return UserType.None;
    }

    public async Task<User?> GetUserById(int id)
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");

        if (response.IsSuccessStatusCode)
        {
            var user = await response.Content.ReadFromJsonAsync<User>();
            return user;
        }

        return null;
    }
}

public record LoginRequest(string Username, string Password);
public record ResetPasswordRequest(string Username, string NewPassword);