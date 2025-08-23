using Banking.DTOs;
using Banking.Models;
using Microsoft.AspNetCore.Mvc;
using Supabase;
using Supabase.Gotrue; // Add this using statement

namespace Banking.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly Supabase.Client _supabase;

    public AuthController(Supabase.Client supabase)
    {
        _supabase = supabase;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        // Now 'SignUpOptions' will be recognized
        var session = await _supabase.Auth.SignUp(registerDto.Email, registerDto.Password, new SignUpOptions
        {
            Data = new Dictionary<string, object> { { "username", registerDto.Username } }
        });

        if (session?.User != null)
        {
            return Ok(new { Message = "User registered successfully! Please check your email to verify." });
        }
        
        return BadRequest("Could not register user.");
    }

[HttpPost("login")]
public async Task<IActionResult> Login(LoginDto loginDto)
{
    var session = await _supabase.Auth.SignIn(loginDto.Email, loginDto.Password);

    // Check that the session, user, and user metadata are not null before accessing them.
    if (session?.AccessToken != null && session.User?.UserMetadata != null)
    {
        return Ok(new AuthResponseDto
        {
            Email = session.User.Email!,
            Username = session.User.UserMetadata["username"].ToString()!,
            Token = session.AccessToken
        });
    }

    return Unauthorized("Invalid credentials.");
}
}