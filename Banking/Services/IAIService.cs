using Banking.Models;

namespace Banking.Services;

public interface IAIService
{
    Task CategorizeTransactionAsync(Transaction transaction);
}