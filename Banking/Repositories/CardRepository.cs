using Banking.Models;
using Supabase;

namespace Banking.Repositories;

public class CardRepository : ICardRepository
{
    private readonly Client _supabase;
    public CardRepository(Client supabase) => _supabase = supabase;

    public async Task<IEnumerable<Card>> GetCardsByUserIdAsync(string userId)
    {
        // Step 1: Get all accounts belonging to the user.
        var userAccountsResponse = await _supabase.From<Account>()
            .Filter("UserId", Supabase.Postgrest.Constants.Operator.Equals, userId)
            .Get();

        if (userAccountsResponse.Models == null || !userAccountsResponse.Models.Any())
        {
            return Enumerable.Empty<Card>();
        }

        // Step 2: Extract the IDs of those accounts.
        var accountIds = userAccountsResponse.Models.Select(a => a.Id.ToString()).ToList();

        // Step 3: Get all cards where the AccountId is in our list of account IDs.
        var cardsResponse = await _supabase.From<Card>()
            .Filter("AccountId", Supabase.Postgrest.Constants.Operator.In, accountIds)
            .Get();

        return cardsResponse.Models ?? Enumerable.Empty<Card>();
    }

    public async Task<Card?> GetCardByIdAsync(Guid cardId)
    {
        return await _supabase.From<Card>()
            .Filter("Id", Supabase.Postgrest.Constants.Operator.Equals, cardId.ToString())
            .Single();
    }

    public async Task UpdateCardAsync(Card card)
    {
        await _supabase.From<Card>().Update(card);
    }
}