using Banking.DTOs;
using Banking.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Banking.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    public AccountController(IAccountService accountService) => _accountService = accountService;

    [HttpGet("details")]
    public async Task<IActionResult> GetAccountDetails()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var accountDetails = await _accountService.GetAccountDetailsAsync(userId);
        return accountDetails == null ? NotFound() : Ok(accountDetails);
    }

    [HttpPost("credit-salary")]
    public async Task<IActionResult> CreditSalary([FromBody] CreditSalaryDto creditSalaryDto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var success = await _accountService.CreditSalaryAsync(userId, creditSalaryDto);
        return !success ? BadRequest("Failed to credit salary.") : Ok(new { message = "Salary credited successfully." });
    }
}