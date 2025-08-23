using Banking.DTOs;

namespace Banking.Services;

public interface ICardService
{
    Task<IEnumerable<CardDto>> GetUserCardsAsync(string userId);
    Task<bool> UpdateCardStatusAsync(Guid cardId, string newStatus, string userId);
}