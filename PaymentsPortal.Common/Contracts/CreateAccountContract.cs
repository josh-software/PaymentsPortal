using System.Net;
using PaymentsPortal.Common.DTOs.Accounts;

namespace PaymentsPortal.Common.Contracts
{
    public class CreateAccountRequest
    {
        public required AccountDto Account { get; set; }
    }

    public class CreateAccountResponse
    {
        public required HttpStatusCode StatusCode { get; set; }

        public required string Message { get; set; }

        public AccountDto? Account { get; set; }
    }
}
