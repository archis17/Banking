using Banking.DTOs;
using Banking.Models;
using Banking.Repositories;

namespace Banking.Services;

public class KycService : IKycService
{
    private readonly IKycRepository _kycRepository;
    public KycService(IKycRepository kycRepository) => _kycRepository = kycRepository;

    public async Task<KycStatusDto?> GetKycStatusAsync(string userId)
    {
        var kycRequest = await _kycRepository.GetLatestKycRequestByUserIdAsync(userId);
        if (kycRequest == null)
        {
            return null;
        }

        return new KycStatusDto
        {
            Status = kycRequest.Status,
            RequestDate = kycRequest.RequestDate,
            VerificationDate = kycRequest.VerificationDate
        };
    }

    public async Task<bool> StartKycProcessAsync(string userId)
    {
        // Business Logic: Check if there's already an approved or pending request.
        var existingRequest = await _kycRepository.GetLatestKycRequestByUserIdAsync(userId);
        if (existingRequest != null && (existingRequest.Status == "Approved" || existingRequest.Status == "Pending"))
        {
            return false; // User cannot start a new process yet.
        }

        // Create a new KYC request for the simulation.
        var newRequest = new KycRequest
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Status = "Pending", // In a real app, this might trigger a 3rd party service.
            RequestDate = DateTime.UtcNow
        };

        await _kycRepository.CreateKycRequestAsync(newRequest);
        return true;
    }
}