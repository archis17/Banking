using Banking.Data;
using Banking.Models;
using Microsoft.EntityFrameworkCore;

namespace Banking.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly ApplicationDbContext _context;
    public AccountRepository(ApplicationDbContext context) => _context = context;

    public async Task<Account?> GetAccountByUserIdAsync(string userId)
    {
        return await _context.Accounts
            .Include(a => a.Transactions.OrderByDescending(t => t.TransactionDate).Take(10))
            .FirstOrDefaultAsync(a => a.UserId == userId);
    }

    public async Task AddTransactionAsync(Transaction transaction)
    {
        await _context.Transactions.AddAsync(transaction);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}