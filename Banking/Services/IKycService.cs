using Banking.DTOs;

namespace Banking.Services;

public interface IKycService
{
    Task<KycStatusDto?> GetKycStatusAsync(string userId);
    Task<bool> StartKycProcessAsync(string userId);
}