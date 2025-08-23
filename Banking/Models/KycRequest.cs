using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Banking.Models;

[Table("KycRequests")]
public class KycRequest : BaseModel
{
    [PrimaryKey("Id")]
    public Guid Id { get; set; }

    [Column("UserId")]
    public string UserId { get; set; } = null!;

    [Column("Status")]
    public string Status { get; set; } = null!; // e.g., "Pending", "Approved", "Rejected"

    [Column("RequestDate")]
    public DateTime RequestDate { get; set; }

    [Column("VerificationDate")]
    public DateTime? VerificationDate { get; set; }
}