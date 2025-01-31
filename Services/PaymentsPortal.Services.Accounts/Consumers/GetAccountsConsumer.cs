using MassTransit;
using Microsoft.EntityFrameworkCore;
using PaymentsPortal.Common.Contracts;
using PaymentsPortal.Services.Accounts.Data;
using PaymentsPortal.Services.Accounts.Mappings;

namespace PaymentsPortal.Services.Accounts.Consumers
{
    public class GetAccountsConsumer : IConsumer<GetAccountsRequest>
    {
        private AccountsDbContext _dbContext;

        public GetAccountsConsumer(AccountsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<GetAccountsRequest> consumer)
        {
            var accounts = await _dbContext.Accounts
                .Select(a => a.ToDto())
                .ToListAsync();

            await consumer.RespondAsync(
                new GetAccountsResponse
                {
                    Accounts = accounts.ToArray()
                }
            );
        }
    }
}
