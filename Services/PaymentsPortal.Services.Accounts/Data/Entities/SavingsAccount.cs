using System.ComponentModel.DataAnnotations.Schema;
using PaymentsPortal.Common.DTOs.Accounts.Enums;

namespace PaymentsPortal.Services.Accounts.Data.Entities
{
    public class SavingsAccount : Account
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal InterestRate { get; set; }

        public SavingsAccount() : base(AccountType.Savings) { }
    }
}




