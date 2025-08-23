using Banking.Models;

namespace Banking.Repositories;

public interface IAccountRepository
{
    Task<Account?> GetAccountByUserIdAsync(string userId);
    Task AddTransactionAsync(Transaction transaction);
    Task UpdateAccountAsync(Account account);
}