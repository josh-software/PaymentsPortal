using Microsoft.EntityFrameworkCore;

namespace PaymentsPortal.Services.Accounts.Data
{
    public static class AccountsDbExtensions
    {
        public static async Task CreateDbIfNotExistsAsync(this IServiceProvider services)
        {
            var context = services.GetRequiredService<AccountsDbContext>();

            // Migrate database
            await context.Database.MigrateAsync();

            // Seed data
            if (!context.Accounts.Any())
            {
                // Test savings account
                context.Accounts.Add(new Entities.SavingsAccount
                {
                    Id = Guid.NewGuid(),
                    Name = "Savings User 1",
                    Balance = 1000,
                    InterestRate = 0.01m
                });

                // Test current account
                context.Accounts.Add(new Entities.CurrentAccount
                {
                    Id = Guid.NewGuid(),
                    Name = "Current User 1",
                    Balance = 1000,
                    OverdraftLimit = 100
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
