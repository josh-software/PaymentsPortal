using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentsPortal.Services.Accounts.Data.Entities
{
    public class SavingsAccount : Account
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal InterestRate { get; set; }

        public SavingsAccount() : base(AccountType.Savings) { }
    }
}




