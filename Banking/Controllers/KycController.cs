using Banking.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Banking.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class KycController : ControllerBase
{
    private readonly IKycService _kycService;
    public KycController(IKycService kycService) => _kycService = kycService;

    [HttpGet("status")]
    public async Task<IActionResult> GetKycStatus()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var status = await _kycService.GetKycStatusAsync(userId);
        if (status == null)
        {
            return NotFound(new { message = "No KYC process has been started for this user." });
        }

        return Ok(status);
    }

    [HttpPost("start")]
    public async Task<IActionResult> StartKycProcess()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var success = await _kycService.StartKycProcessAsync(userId);
        if (!success)
        {
            return BadRequest(new { message = "A KYC process is already pending or approved for this user." });
        }

        return Ok(new { message = "KYC process started successfully. Status is now Pending." });
    }
}