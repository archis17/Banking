using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;

namespace Banking.Models;

[Table("Transactions")]
public class Transaction : BaseModel
{
    [PrimaryKey("Id")]
    public Guid Id { get; set; }

    [Column("Amount")]
    public decimal Amount { get; set; }

    [Column("TransactionDate")]
    public DateTime TransactionDate { get; set; }

    [Column("Type")]
    public string Type { get; set; } = null!; // Changed from required

    [Column("Description")]
    public string Description { get; set; } = null!; // Changed from required

    [Column("Category")]
    public string? Category { get; set; }

    [Column("AccountId")]
    public Guid AccountId { get; set; }
}