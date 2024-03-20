using SecuritiesApplication.Entities;
using SecuritiesApplication.Helpers;

namespace SecuritiesApplication.Repositories
{
    public class SecurityRepository : ISecurityRepository
    {
        private readonly SecurityDbContext _dbContext;
        private readonly ILoggerInternal _logger;

        public SecurityRepository(SecurityDbContext dbContext, ILoggerInternal logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task StoreSecurities(IEnumerable<Security> securities)
        {
            try
            {
                await _dbContext.Securities.AddRangeAsync(securities);

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an exception when trying to store data securities", ex);
                throw;
            }
        }
    }
}
