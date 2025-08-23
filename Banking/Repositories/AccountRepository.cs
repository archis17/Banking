using Banking.Models;
using Supabase;
using System.Threading.Tasks; // Make sure this is included for Task

namespace Banking.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly Client _supabase;
        public AccountRepository(Client supabase) => _supabase = supabase;

        public async Task<Account?> GetAccountByUserIdAsync(string userId)
        {
            var response = await _supabase.From<Account>()
                .Filter("UserId", Supabase.Postgrest.Constants.Operator.Equals, userId)
                .Single();

            if (response != null)
            {
                var transactionsResponse = await _supabase.From<Transaction>()
                    .Filter("AccountId", Supabase.Postgrest.Constants.Operator.Equals, response.Id.ToString())
                    .Limit(10)
                    .Order("TransactionDate", Supabase.Postgrest.Constants.Ordering.Descending)
                    .Get();
                response.Transactions = transactionsResponse.Models;
            }

            return response;
        }

        /// <summary>
        /// Adds a new transaction record to the database.
        /// </summary>
        /// <param name="transaction">The transaction object to be inserted.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task AddTransactionAsync(Transaction transaction)
        {
            // The Insert method adds the new transaction object to the 'transactions' table.
            await _supabase.From<Transaction>().Insert(transaction);
        }

        public Task UpdateAccountAsync(Account account)
        {
            throw new NotImplementedException();
        }
    }
}