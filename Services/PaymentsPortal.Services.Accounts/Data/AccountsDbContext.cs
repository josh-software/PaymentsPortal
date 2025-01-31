using Microsoft.EntityFrameworkCore;
using PaymentsPortal.Common.DTOs.Accounts.Enums;
using PaymentsPortal.Services.Accounts.Data.Entities;

namespace PaymentsPortal.Services.Accounts.Data
{
    public class AccountsDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<CurrentAccount> CurrentAccounts { get; set; }
        public DbSet<SavingsAccount> SavingsAccounts { get; set; }

        public AccountsDbContext(DbContextOptions<AccountsDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasDiscriminator<AccountType>("AccountType")
                .HasValue<CurrentAccount>(AccountType.Current)
                .HasValue<SavingsAccount>(AccountType.Savings);
        }
    }
}
