using Banking.DTOs;
using Banking.Models;
using Banking.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;

    public AuthController(UserManager<User> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        var user = new User { UserName = registerDto.Username, Email = registerDto.Email };
        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (result.Succeeded)
        {
            return Ok(new { Message = "User registered successfully!" });
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(error.Code, error.Description);
        }

        return BadRequest(ModelState);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null)
        {
            return Unauthorized("Invalid credentials.");
        }

        var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!result)
        {
            return Unauthorized("Invalid credentials.");
        }

        return new AuthResponseDto
        {
            Email = user.Email!,
            Username = user.UserName!,
            Token = _tokenService.CreateToken(user)
        };
    }
}