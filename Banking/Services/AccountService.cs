using Banking.DTOs;
using Banking.Models;
using Banking.Repositories;

namespace Banking.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IAIService _aiService; // 1. Add the AI service dependency

    // 2. Inject the IAIService in the constructor
    public AccountService(IAccountRepository accountRepository, IAIService aiService)
    {
        _accountRepository = accountRepository;
        _aiService = aiService;
    }

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
            Id = Guid.NewGuid(),
            AccountId = account.Id,
            Amount = creditSalaryDto.Amount,
            TransactionDate = DateTime.UtcNow,
            Type = "Credit",
            Description = creditSalaryDto.Description
            // Category will now be set by the AI service
        };
        
        // 3. Call the AI service to categorize the transaction before saving it.
        await _aiService.CategorizeTransactionAsync(transaction);
        
        await _accountRepository.AddTransactionAsync(transaction);
        await _accountRepository.UpdateAccountAsync(account);

        return true;
    }
}