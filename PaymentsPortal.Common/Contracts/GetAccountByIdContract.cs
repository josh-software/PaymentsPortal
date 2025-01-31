using PaymentsPortal.Common.DTOs.Accounts;

namespace PaymentsPortal.Common.Contracts
{
    public class GetAccountByIdRequest
    {
        public Guid Id { get; set; }
    }

    public class GetAccountByIdResponse
    {
        public AccountDto? Account { get; set; }
    }
}
