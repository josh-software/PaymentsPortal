namespace PaymentsPortal.Server.Models
{
    public class Account
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
        public decimal Balance { get; set; }
        public bool IsFrozen { get; set; } = false;
        public required string AccountType { get; set; }

        // Savings Account
        public decimal? InterestRate { get; set; }

        // Current Account
        public decimal? OverdraftLimit { get; set; }
    }
}
