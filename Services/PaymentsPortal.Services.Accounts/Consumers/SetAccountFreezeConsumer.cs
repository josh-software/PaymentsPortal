using System.Net;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using PaymentsPortal.Common.Contracts;
using PaymentsPortal.Services.Accounts.Data;

namespace PaymentsPortal.Services.Accounts.Consumers
{
    public class SetAccountFreezeConsumer : IConsumer<SetAccountFreezeRequest>
    {
        private AccountsDbContext _dbContext;

        public SetAccountFreezeConsumer(AccountsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<SetAccountFreezeRequest> consumer)
        {
            var accountId = consumer.Message.Id;
            var account = await _dbContext.Accounts
                .FirstOrDefaultAsync(a => a.Id == accountId);

            if (account == null)
            {
                await consumer.RespondAsync(
                    new SetAccountFreezeResponse
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "Account not found"
                    }
                );
                return;
            }

            if (account.IsFrozen == consumer.Message.IsFrozen)
            {
                await consumer.RespondAsync(
                    new SetAccountFreezeResponse
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "Account freeze status not changed"
                    }
                );
                return;
            }

            account.IsFrozen = consumer.Message.IsFrozen;
            await _dbContext.SaveChangesAsync();
            await consumer.RespondAsync(
                new SetAccountFreezeResponse
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Account freeze status updated"
                }
            );
        }
    }
}
