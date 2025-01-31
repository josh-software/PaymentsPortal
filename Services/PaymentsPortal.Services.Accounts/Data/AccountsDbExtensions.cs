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
                context.Accounts.Add(new Entities.Account { Id = Guid.NewGuid(), Name = "TestUser1" });
                await context.SaveChangesAsync();
            }
        }
    }
}
