using MassTransit;
using Microsoft.EntityFrameworkCore;
using PaymentsPortal.Common.Contracts;
using PaymentsPortal.Services.Accounts.Data;
using PaymentsPortal.Services.Accounts.Mappings;

namespace PaymentsPortal.Services.Accounts.Consumers
{
    public class GetAccountByIdConsumer : IConsumer<GetAccountByIdRequest>
    {
        private AccountsDbContext _dbContext;

        public GetAccountByIdConsumer(AccountsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<GetAccountByIdRequest> consumer)
        {
            var accountId = consumer.Message.Id;
            var account = await _dbContext.Accounts
                .FirstOrDefaultAsync(a => a.Id == accountId);

            await consumer.RespondAsync(
                new GetAccountByIdResponse
                {
                    Account = account?.ToDto()
                }
            );
        }
    }
}
