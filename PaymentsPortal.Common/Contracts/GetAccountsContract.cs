using PaymentsPortal.Common.DTOs.Accounts;

namespace PaymentsPortal.Common.Contracts
{
    public class GetAccountsRequest { }

    public class GetAccountsResponse
    {
        public required AccountDto[] Accounts { get; set; }
    }
}
