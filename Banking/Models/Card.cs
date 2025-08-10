namespace Banking.Models;

public class Card : BaseEntity
{
    public required string CardNumber { get; set; }
    public required string CardHolderName { get; set; }
    public DateTime ExpiryDate { get; set; }
    public required string Cvv { get; set; }

    // Using a string for status. E.g., "Active", "Frozen", "Locked"
    public required string Status { get; set; }
    public required string Type { get; set; }

    // Foreign Key to the Account model
    public Guid AccountId { get; set; }

    // Navigation Property
    public required virtual Account Account { get; set; }
}