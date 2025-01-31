using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentsPortal.Services.Accounts.Data.Entities
{
    public abstract class Account
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }

        public bool IsFrozen { get; set; } = false;

        [Required]
        public AccountType AccountType { get; protected set; } // Make setter protected

        protected Account(AccountType accountType)
        {
            AccountType = accountType;
        }
    }
}
