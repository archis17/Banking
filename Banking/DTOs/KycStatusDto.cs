namespace Banking.DTOs;

public class KycStatusDto
{
    public required string Status { get; set; }
    public DateTime RequestDate { get; set; }
    public DateTime? VerificationDate { get; set; }
}