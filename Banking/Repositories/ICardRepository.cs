using Banking.Models;

namespace Banking.Repositories;

public interface ICardRepository
{
    Task<IEnumerable<Card>> GetCardsByUserIdAsync(string userId);
    Task<Card?> GetCardByIdAsync(Guid cardId);
    Task<bool> SaveChangesAsync();
}