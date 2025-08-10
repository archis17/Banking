using Banking.DTOs;
using Banking.Models;
using Banking.Repositories;

namespace Banking.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    public AccountService(IAccountRepository accountRepository) => _accountRepository = accountRepository;

    public async Task<AccountDetailsDto?> GetAccountDetailsAsync(string userId)
    {
        var account = await _accountRepository.GetAccountByUserIdAsync(userId);
        if (account == null) return null;

        return new AccountDetailsDto
        {
            AccountNumber = account.AccountNumber,
            Balance = account.Balance,
            AccountType = account.AccountType,
            RecentTransactions = account.Transactions.Select(t => new TransactionDto
            {
                Amount = t.Amount,
                TransactionDate = t.TransactionDate,
                Type = t.Type,
                Description = t.Description,
                Category = t.Category
            }).ToList()
        };
    }

    public async Task<bool> CreditSalaryAsync(string userId, CreditSalaryDto creditSalaryDto)
    {
        var account = await _accountRepository.GetAccountByUserIdAsync(userId);
        if (account == null || creditSalaryDto.Amount <= 0) return false;

        account.Balance += creditSalaryDto.Amount;
        var transaction = new Transaction
        {
            AccountId = account.Id,
            Amount = creditSalaryDto.Amount,
            TransactionDate = DateTime.UtcNow,
            Type = "Credit",
            Description = creditSalaryDto.Description,
            Category = "Salary"
        };
        await _accountRepository.AddTransactionAsync(transaction);
        return await _accountRepository.SaveChangesAsync();
    }
}