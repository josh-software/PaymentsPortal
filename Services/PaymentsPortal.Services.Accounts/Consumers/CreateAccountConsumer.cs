using System.Net;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using PaymentsPortal.Common.Contracts;
using PaymentsPortal.Services.Accounts.Data;
using PaymentsPortal.Services.Accounts.Mappings;

namespace PaymentsPortal.Services.Accounts.Consumers
{
    public class CreateAccountConsumer : IConsumer<CreateAccountRequest>
    {
        private readonly AccountsDbContext _dbContext;

        public CreateAccountConsumer(AccountsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //TODO: Add validation
        public async Task Consume(ConsumeContext<CreateAccountRequest> consumer)
        {
            var account = consumer.Message.Account;

            if (account == null)
            {
                await consumer.RespondAsync(new CreateAccountResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Invalid request: Account details are required."
                });
                return;
            }

            // Check if an account with the same identifier already exists - TODO: Implement a more robust check, maybe by email
            bool accountExists = await _dbContext.Accounts.AnyAsync(a => a.Id == account.Id);
            if (accountExists)
            {
                await consumer.RespondAsync(new CreateAccountResponse
                {
                    StatusCode = HttpStatusCode.Conflict, // 409 Conflict
                    Message = "An account with this ID already exists."
                });
                return;
            }

            try
            {
                _dbContext.Accounts.Add(account.FromDto()); // From DTO generates a new Account Id
                await _dbContext.SaveChangesAsync();

                var createdAccount = await _dbContext.Accounts
                    .FirstOrDefaultAsync(a => a.Id == account.Id)
                    ?? throw new Exception("Error creating account");

                await consumer.RespondAsync(new CreateAccountResponse
                {
                    StatusCode = HttpStatusCode.Created, // 201 Created
                    Message = "Account created successfully",
                    Account = createdAccount.ToDto()
                });
            }
            catch (Exception ex)
            {
                await consumer.RespondAsync(new CreateAccountResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError, // 500 Internal Server Error
                    Message = $"Error creating account: {ex.Message}"
                });
            }
        }
    }

}
