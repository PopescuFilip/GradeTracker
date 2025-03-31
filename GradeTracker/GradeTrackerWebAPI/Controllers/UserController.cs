using GradeTrackerWebAPI.Enums;
using GradeTrackerWebAPI.Models;
using GradeTrackerWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradeTrackerWebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IEntityService<UserEntity> entityService, IUserService userService) : BaseEntityController<UserEntity>(entityService)
{
    private readonly IUserService _userService = userService;

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

public record LoginRequest(string Username, string Password);
public record ResetPasswordRequest(string Username, string NewPassword);