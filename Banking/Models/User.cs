using Microsoft.AspNetCore.Identity;

namespace Banking.Models;

public class User : IdentityUser
{
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}