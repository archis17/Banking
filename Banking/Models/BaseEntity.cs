using System.ComponentModel.DataAnnotations;

namespace Banking.Models;

public abstract class BaseEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
}