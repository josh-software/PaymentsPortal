using System.Net;
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

        private readonly IRequestClient<GetAccountByIdRequest> _getAccountByIdClient;

        private readonly IRequestClient<CreateAccountRequest> _createAccountClient;

        private readonly IRequestClient<SetAccountFreezeRequest> _setAccountFreezeClient;

        private readonly ILogger<AccountsController> _logger;

        public AccountsController(
            IRequestClient<GetAccountsRequest> getAccountsClient,
            IRequestClient<GetAccountByIdRequest> getAccountByIdClient,
            IRequestClient<CreateAccountRequest> createAccountClient,
            IRequestClient<SetAccountFreezeRequest> setAccountFreezeClient,
            ILogger<AccountsController> logger)
        {
            _getAccountsClient = getAccountsClient;
            _getAccountByIdClient = getAccountByIdClient;
            _createAccountClient = createAccountClient;
            _setAccountFreezeClient = setAccountFreezeClient;
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

        [HttpGet("{id}", Name = "GetAccountById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _getAccountByIdClient
                .GetResponse<GetAccountByIdResponse>(new GetAccountByIdRequest { Id = id });

            var account = response.Message.Account?.ToAccount();

            return account is null
                ? NotFound()
                : Ok(account);
        }

        [HttpPost(Name = "CreateAccount")]
        public async Task<IActionResult> Post([FromBody] Account account)
        {
            var response = await _createAccountClient
                .GetResponse<CreateAccountResponse>(new CreateAccountRequest
                {
                    Account = account.ToDto()
                });

            if (response.Message.Account is null)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error creating account");
            }

            return response.Message.StatusCode switch
            {
                HttpStatusCode.Created => CreatedAtAction(nameof(GetById), new { id = response.Message.Account.Id }, response.Message.Account),
                HttpStatusCode.Conflict => Conflict(response.Message.Message),
                HttpStatusCode.BadRequest => BadRequest(response.Message.Message),
                _ => StatusCode((int)response.Message.StatusCode, response.Message.Message)
            };
        }

        [HttpPut("{id}/freeze", Name = "SetAccountFreeze")]
        public async Task<IActionResult> Freeze(Guid id, [FromBody] bool isFrozen)
        {
            var response = await _setAccountFreezeClient
                .GetResponse<SetAccountFreezeResponse>(new SetAccountFreezeRequest
                {
                    Id = id,
                    IsFrozen = isFrozen
                });
            return response.Message.StatusCode switch
            {
                HttpStatusCode.OK => Ok(response.Message.Message),
                HttpStatusCode.NotFound => NotFound(response.Message.Message),
                _ => StatusCode((int)response.Message.StatusCode, response.Message.Message)
            };
        }
    }
}
