using MassTransit;
using Microsoft.AspNetCore.Mvc;
using PaymentsPortal.Common.Contracts;
using PaymentsPortal.Server.Mappings;
using PaymentsPortal.Server.Models;

namespace PaymentsPortal.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IRequestClient<GetAccountsRequest> _getAccountsClient;

        private readonly ILogger<AccountsController> _logger;

        public AccountsController(
            IRequestClient<GetAccountsRequest> getAccountsClient,
            ILogger<AccountsController> logger)
        {
            _getAccountsClient = getAccountsClient;
            _logger = logger;
        }

        [HttpGet(Name = "GetAccounts")]
        public async Task<IEnumerable<Account>> Get()
        {
            var response = await _getAccountsClient
                .GetResponse<GetAccountsResponse>(new GetAccountsRequest());

            return response.Message.Accounts
                .Select(w => w.ToAccount());
        }
    }
}
