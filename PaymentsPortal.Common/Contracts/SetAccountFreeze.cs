using System.Net;

namespace PaymentsPortal.Common.Contracts
{
    public class SetAccountFreezeRequest
    {
        public required Guid Id { get; set; }
        public required bool IsFrozen { get; set; }
    }

    public class SetAccountFreezeResponse
    {
        public required HttpStatusCode StatusCode { get; set; }

        public required string Message { get; set; }
    }
}
