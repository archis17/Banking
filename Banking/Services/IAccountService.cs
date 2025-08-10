using Banking.DTOs;

namespace Banking.Services;

public interface IAccountService
{
    Task<AccountDetailsDto?> GetAccountDetailsAsync(string userId);
    Task<bool> CreditSalaryAsync(string userId, CreditSalaryDto creditSalaryDto);
}