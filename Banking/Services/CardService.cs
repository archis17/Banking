using Banking.DTOs;
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
            // Business logic to mask the card number
            MaskedCardNumber = $"************{card.CardNumber.Substring(card.CardNumber.Length - 4)}",
            CardHolderName = card.CardHolderName,
            ExpiryDate = card.ExpiryDate,
            Status = card.Status,
            Type = card.Type
        });
    }

    public async Task<bool> UpdateCardStatusAsync(Guid cardId, string newStatus, string userId)
    {
        var card = await _cardRepository.GetCardByIdAsync(cardId);

        // Business logic: Ensure the card exists and belongs to the current user
        if (card == null || card.Account.UserId != userId)
        {
            return false;
        }

        // Business logic: Validate the new status if necessary
        if (newStatus != "Active" && newStatus != "Frozen")
        {
            return false; // Or throw a specific exception
        }

        card.Status = newStatus;
        return await _cardRepository.SaveChangesAsync();
    }
}