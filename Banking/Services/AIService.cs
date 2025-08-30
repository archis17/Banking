using Banking.Models;

namespace Banking.Services;

public class AIService : IAIService
{
    // In the future, you will inject your ML.NET model or a repository here.
    public AIService() { }

    public Task CategorizeTransactionAsync(Transaction transaction)
    {
        // Placeholder Logic: In the next step, you will replace this with a
        // call to your ML.NET model to predict a category based on the transaction description.
        
        // Example of simple rule-based logic for now:
        if (transaction.Description.Contains("Grocery"))
        {
            transaction.Category = "Food";
        }
        else if (transaction.Description.Contains("Rent"))
        {
            transaction.Category = "Utilities";
        }
        else
        {
            transaction.Category = "General";
        }
        
        Console.WriteLine($"Transaction {transaction.Id} categorized as {transaction.Category}");
        
        // You would then save this updated transaction back to the database.
        return Task.CompletedTask;
    }
}