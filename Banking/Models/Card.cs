using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;
namespace Banking.Models;

[Table("Cards")]
public class Card : BaseModel
{
    [PrimaryKey("Id")]
    public Guid Id { get; set; }

    [Column("CardNumber")]
    public string CardNumber { get; set; } = null!;

    [Column("CardHolderName")]
    public string CardHolderName { get; set; } = null!;

    [Column("ExpiryDate")]
    public DateTime ExpiryDate { get; set; }

    [Column("Cvv")]
    public string Cvv { get; set; } = null!;

    [Column("Status")]
    public string Status { get; set; } = null!;

    [Column("Type")]
    public string Type { get; set; } = null!;

    [Column("AccountId")]
    public Guid AccountId { get; set; }
}