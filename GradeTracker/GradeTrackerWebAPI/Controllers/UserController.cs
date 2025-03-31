using GradeTrackerWebAPI.Enums;
using GradeTrackerWebAPI.Models;
using GradeTrackerWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradeTrackerWebAPI.Controllers;

/// <summary>
/// Controller for managing user-related actions such as login, password reset, and retrieving user type.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UserController(IEntityService<UserEntity> entityService, IUserService userService) : BaseEntityController<UserEntity>(entityService)
{
    private readonly IUserService _userService = userService;

    /// <summary>
    /// Authenticates a user based on the provided username and password.
    /// </summary>
    /// <param name="loginRequest">The login credentials (username and password).</param>
    /// <returns>A user entity if authentication is successful; otherwise, an unauthorized status.</returns>
    [HttpPost("login")]
    public async Task<ActionResult<UserEntity?>> Login([FromBody] LoginRequest loginRequest)
    {
        var user = await _userService.Login(loginRequest.Username, loginRequest.Password);
        if (user == null)
        {
            return Unauthorized("Invalid username or password.");
        }
        return Ok(user);
    }

    /// <summary>
    /// Resets the password for a user.
    /// </summary>
    /// <param name="resetPasswordRequest">The request containing username and the new password.</param>
    /// <returns>A boolean indicating whether the password reset was successful.</returns>
    [HttpPost("reset-password")]
    public async Task<ActionResult<bool>> ResetPassword([FromBody] ResetPasswordRequest resetPasswordRequest)
    {
        var result = await _userService.ResetPassword(resetPasswordRequest.Username, resetPasswordRequest.NewPassword);
        if (!result)
        {
            return BadRequest("Failed to change password. Please verify your credentials.");
        }
        return Ok(true);
    }

    /// <summary>
    /// Retrieves the type of a user based on their ID.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <returns>The type of the user.</returns>
    [HttpGet("user-type/{id}")]
    public async Task<ActionResult<UserType>> GetUserType(int id)
    {
        var userType = await _userService.GetUserType(id);
        if (userType == null)
        {
            return NotFound("User type not found.");
        }
        return Ok(userType);
    }
}

/// <summary>
/// Represents the request data for user login.
/// </summary>
public record LoginRequest(string Username, string Password);

/// <summary>
/// Represents the request data for password reset.
/// </summary>
public record ResetPasswordRequest(string Username, string NewPassword);
