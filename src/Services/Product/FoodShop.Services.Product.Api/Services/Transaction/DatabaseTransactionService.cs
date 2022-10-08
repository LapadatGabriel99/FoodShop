using FoodShop.Services.Product.Api.Data;
using FoodShop.Services.Product.Api.Services.Contracts.Transaction;

namespace FoodShop.Services.Product.Api.Services.Transaction
{
    public sealed class DatabaseTransactionService : IDatabaseTransactionService<ApplicationDbContext>
    {
        private readonly ILogger<DatabaseTransactionService> _logger;
        private readonly ApplicationDbContext _dbContext;

        public DatabaseTransactionService(ILogger<DatabaseTransactionService> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task Execute(Action action)
        {
            using(_dbContext)
            {
                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        if (action != null)
                        {
                            action();
                        }

                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError(ex.Message);
                    }
                }
            }
        }
    }
}
