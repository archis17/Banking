using System.ComponentModel.DataAnnotations.Schema;

namespace Banking.Models;

public class Transaction : BaseEntity
{
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Amount { get; set; }

    public DateTime TransactionDate { get; set; }

    public required string Type { get; set; } // "Credit" or "Debit"

    public required string Description { get; set; }

    public string? Category { get; set; }

    // Foreign Key to the Account model
    public Guid AccountId { get; set; }

    // Navigation Property - DO NOT mark this as required
    public virtual Account Account { get; set; } = null!;
}