using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;

namespace Banking.Models;

public class Account : BaseEntity
{
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Balance { get; set; }
    public required string AccountNumber { get; set; }
    public required string AccountType { get; set; }
    public required string UserId { get; set; }
    public required virtual User User { get; set; }
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();
}