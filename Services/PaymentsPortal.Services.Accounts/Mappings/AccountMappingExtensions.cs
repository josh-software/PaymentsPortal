using PaymentsPortal.Common.DTOs.Accounts;
using PaymentsPortal.Services.Accounts.Data.Entities;

namespace PaymentsPortal.Services.Accounts.Mappings
{
    public static class AccountMappingExtensions
    {
        public static AccountDto ToDto(this Account account)
        {
            return account switch
            {
                CurrentAccount currentAccount => currentAccount.ToDto(),
                SavingsAccount savingsAccount => savingsAccount.ToDto(),
                _ => throw new System.ArgumentOutOfRangeException(nameof(account), account, null)
            };
        }

        private static AccountDto ToDto(this CurrentAccount currentAccount)
        {
            return new AccountDto
            {
                Id = currentAccount.Id,
                Name = currentAccount.Name,
                Balance = currentAccount.Balance,
                IsFrozen = currentAccount.IsFrozen,
                AccountType = currentAccount.AccountType,
                OverdraftLimit = currentAccount.OverdraftLimit
            };
        }

        private static AccountDto ToDto(this SavingsAccount savingsAccount)
        {
            return new AccountDto
            {
                Id = savingsAccount.Id,
                Name = savingsAccount.Name,
                Balance = savingsAccount.Balance,
                IsFrozen = savingsAccount.IsFrozen,
                AccountType = savingsAccount.AccountType,
                InterestRate = savingsAccount.InterestRate
            };
        }
    }
}
