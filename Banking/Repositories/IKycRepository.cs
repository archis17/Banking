using Banking.Models;

namespace Banking.Repositories;

public interface IKycRepository
{
    Task<KycRequest?> GetLatestKycRequestByUserIdAsync(string userId);
    Task CreateKycRequestAsync(KycRequest request);
}