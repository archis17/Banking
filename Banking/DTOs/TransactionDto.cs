namespace Banking.DTOs;

public class TransactionDto
{
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public required string Type { get; set; }
    public required string Description { get; set; }
    public string? Category { get; set; }
}