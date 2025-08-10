using Banking.Models;

namespace Banking.Services;

public interface ITokenService
{
    string CreateToken(User user);
}