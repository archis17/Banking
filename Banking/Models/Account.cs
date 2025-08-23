using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;

namespace Banking.Models;

[Table("Accounts")]
public class Account : BaseModel
{
    [PrimaryKey("Id")]
    public Guid Id { get; set; }

    [Column("Balance")]
    public decimal Balance { get; set; }

    [Column("AccountNumber")]
    public string AccountNumber { get; set; } = null!; // Changed from required

    [Column("AccountType")]
    public string AccountType { get; set; } = null!; // Changed from required

    [Column("UserId")]
    public string UserId { get; set; } = null!; // Changed from required
    
    public List<Transaction> Transactions { get; set; } = new();
}