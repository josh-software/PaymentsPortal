using PaymentsPortal.Common.DTOs.Accounts;
using PaymentsPortal.Common.DTOs.Accounts.Enums;
using PaymentsPortal.Server.Models;

namespace PaymentsPortal.Server.Mappings
{
    public static class AccountMappingExtensions
    {
        public static Account ToAccount(this AccountDto accountDto)
        {
            var accountTypeString = Enum.GetName(typeof(AccountType), value: accountDto.AccountType);
            if (accountTypeString is null)
            {
                throw new ArgumentOutOfRangeException(nameof(accountDto.AccountType), accountDto.AccountType, null);
            }

            return new Account
            {
                Id = accountDto.Id,
                Name = accountDto.Name,
                Balance = accountDto.Balance,
                IsFrozen = accountDto.IsFrozen,
                AccountType = accountTypeString,
                InterestRate = accountDto.InterestRate,
                OverdraftLimit = accountDto.OverdraftLimit
            };
        }
    }
}
