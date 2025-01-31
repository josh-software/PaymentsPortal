using PaymentsPortal.Common.DTOs.Accounts;
using PaymentsPortal.Common.DTOs.Accounts.Enums;
using PaymentsPortal.Services.Accounts.Data.Entities;

namespace PaymentsPortal.Services.Accounts.Mappings
{
    public static class AccountMappingExtensions
    {
        public static Account FromDto(this AccountDto accountDto)
        {
            return accountDto.AccountType switch
            {
                AccountType.Current => new CurrentAccount
                {
                    Id = accountDto.Id ?? Guid.NewGuid(),
                    Name = accountDto.Name,
                    Balance = accountDto.Balance,
                    IsFrozen = accountDto.IsFrozen,
                    OverdraftLimit = accountDto.OverdraftLimit ?? throw new Exception("Overdraft limit is required for current accounts")
                },
                AccountType.Savings => new SavingsAccount
                {
                    Id = accountDto.Id ?? Guid.NewGuid(),
                    Name = accountDto.Name,
                    Balance = accountDto.Balance,
                    IsFrozen = accountDto.IsFrozen,
                    InterestRate = accountDto.InterestRate ?? throw new Exception("Interest Rate is required for savings accounts")
                },
                _ => throw new ArgumentOutOfRangeException(nameof(accountDto.AccountType), accountDto.AccountType, null)
            };
        }

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
