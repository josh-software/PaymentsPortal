using PaymentsPortal.Common.DTOs.Accounts.Enums;

namespace PaymentsPortal.Common.DTOs.Accounts
{
    public class AccountDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public bool IsFrozen { get; set; } = false;
        public AccountType AccountType { get; set; }

        // Savings Account
        public decimal? InterestRate { get; set; }

        // Current Account
        public decimal? OverdraftLimit { get; set; }
    }
}
