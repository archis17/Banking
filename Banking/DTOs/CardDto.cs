namespace Banking.DTOs;

public class CardDto
{
    public Guid Id { get; set; }
    public required string MaskedCardNumber { get; set; } // e.g., "************4444"
    public required string CardHolderName { get; set; }
    public DateTime ExpiryDate { get; set; }
    public required string Status { get; set; }
    public required string Type { get; set; }
}