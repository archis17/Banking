using System.ComponentModel.DataAnnotations;

namespace Banking.DTOs;

public class UpdateCardStatusDto
{
    [Required]
    public required string NewStatus { get; set; } // e.g., "Frozen", "Active"
}