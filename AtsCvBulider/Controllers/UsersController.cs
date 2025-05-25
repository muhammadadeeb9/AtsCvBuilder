using AtsCvBuilder.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AtsCvBuilder.DTOs;

namespace AtsCvBuilder.Controllers;

// Controllers/UserController.cs
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    // GET: api/user
    [HttpGet]
    public async Task<IActionResult> GetUserInfo()
    {
        var userId = _userManager.GetUserId(User);
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return NotFound(new { Message = "User not found." });

        return Ok(new
        {
            FullName = user.FullName,
            DateOfBirth = user.DateOfBirth,
            Description = user.Description
        });
    }

    // PUT: api/user
    [HttpPut]
    public async Task<IActionResult> UpdateUserInfo([FromBody] UpdateProfileDto model)
    {
        var userId = _userManager.GetUserId(User);
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return NotFound(new { Message = "User not found." });

        user.FullName = model.FullName;
        user.DateOfBirth = model.DateOfBirth;
        user.Description = model.Description;

        await _userManager.UpdateAsync(user);

        return Ok(new { Message = "User information updated successfully." });
    }

    // PUT: api/user/change-password
    [HttpPut("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
    {
        var userId = _userManager.GetUserId(User);
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return NotFound(new { Message = "User not found." });

        var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

        if (result.Succeeded)
            return Ok(new { Message = "Password changed successfully." });

        return BadRequest(result.Errors);
    }
}