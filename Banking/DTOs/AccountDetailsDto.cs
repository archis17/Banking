namespace Banking.DTOs;

public class AccountDetailsDto
{
    public required string AccountNumber { get; set; }
    public decimal Balance { get; set; }
    public required string AccountType { get; set; }
    public List<TransactionDto> RecentTransactions { get; set; } = new();
}