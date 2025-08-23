using Banking.Data;
using Banking.Models;
using Microsoft.EntityFrameworkCore;

namespace Banking.Repositories;

public class CardRepository : ICardRepository
{
    private readonly ApplicationDbContext _context;
    public CardRepository(ApplicationDbContext context) => _context = context;

    public async Task<IEnumerable<Card>> GetCardsByUserIdAsync(string userId)
    {
        return await _context.Cards
            .Where(c => c.Account.UserId == userId)
            .ToListAsync();
    }

    public async Task<Card?> GetCardByIdAsync(Guid cardId)
    {
        return await _context.Cards.FindAsync(cardId);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}