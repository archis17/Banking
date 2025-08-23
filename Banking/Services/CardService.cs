using Banking.DTOs;
using Banking.Models;
using Banking.Repositories;

namespace Banking.Services;

public class CardService : ICardService
{
    private readonly ICardRepository _cardRepository;
    public CardService(ICardRepository cardRepository) => _cardRepository = cardRepository;

    public async Task<IEnumerable<CardDto>> GetUserCardsAsync(string userId)
    {
        var cards = await _cardRepository.GetCardsByUserIdAsync(userId);

        return cards.Select(card => new CardDto
        {
            Id = card.Id,
            MaskedCardNumber = $"************{card.CardNumber.Substring(card.CardNumber.Length - 4)}",
            CardHolderName = card.CardHolderName,
            ExpiryDate = card.ExpiryDate,
            Status = card.Status,
            Type = card.Type
        });
    }

    public async Task<bool> UpdateCardStatusAsync(Guid cardId, string newStatus, string userId)
    {
        // Step 1: Get all cards owned by the user.
        var userCards = await _cardRepository.GetCardsByUserIdAsync(userId);

        // Step 2: Find the specific card to update within that list.
        // This implicitly verifies that the user owns the card.
        var cardToUpdate = userCards.FirstOrDefault(c => c.Id == cardId);

        if (cardToUpdate == null)
        {
            // The card doesn't exist or the user doesn't own it.
            return false;
        }

        // Step 3: Validate and update the status.
        if (newStatus != "Active" && newStatus != "Frozen")
        {
            return false;
        }

        cardToUpdate.Status = newStatus;
        await _cardRepository.UpdateCardAsync(cardToUpdate);
        return true;
    }
}