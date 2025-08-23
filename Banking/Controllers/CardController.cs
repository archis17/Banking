using Banking.DTOs;
using Banking.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Banking.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CardController : ControllerBase
{
    private readonly ICardService _cardService;
    public CardController(ICardService cardService) => _cardService = cardService;

    [HttpGet]
    public async Task<IActionResult> GetUserCards()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var cards = await _cardService.GetUserCardsAsync(userId);
        return Ok(cards);
    }

    [HttpPatch("{cardId}/status")]
    public async Task<IActionResult> UpdateCardStatus(Guid cardId, [FromBody] UpdateCardStatusDto updateDto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var success = await _cardService.UpdateCardStatusAsync(cardId, updateDto.NewStatus, userId);

        if (!success)
        {
            return BadRequest("Could not update card status. Please check the card ID or status value.");
        }

        return Ok(new { message = "Card status updated successfully." });
    }
}