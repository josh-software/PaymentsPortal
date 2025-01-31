using Microsoft.EntityFrameworkCore;

namespace PaymentsPortal.Services.Accounts.Data
{
    public class AccountsDbContext : DbContext
    {
        public AccountsDbContext(DbContextOptions<AccountsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Entities.Account> Accounts { get; set; }
    }
}
