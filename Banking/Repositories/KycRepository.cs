using Banking.Models;
using Supabase;

namespace Banking.Repositories;

public class KycRepository : IKycRepository
{
    private readonly Client _supabase;
    public KycRepository(Client supabase) => _supabase = supabase;

    public async Task<KycRequest?> GetLatestKycRequestByUserIdAsync(string userId)
    {
        var response = await _supabase.From<KycRequest>()
            .Filter("UserId", Postgrest.Constants.Operator.Equals, userId)
            .Order("RequestDate", Postgrest.Constants.Ordering.Descending)
            .Limit(1)
            .Get();

        return response.Models.FirstOrDefault();
    }

    public async Task CreateKycRequestAsync(KycRequest request)
    {
        await _supabase.From<KycRequest>().Insert(request);
    }
}