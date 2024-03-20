using SecuritiesApplication.ApiClients;
using SecuritiesApplication.Entities;
using SecuritiesApplication.Helpers;
using SecuritiesApplication.Repositories;

namespace SecuritiesApplication.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IPriceApiClient _priceApiClient;
        private readonly ISecurityRepository _securityRepository;
        private readonly ILoggerInternal _logger;
        public SecurityService(IPriceApiClient priceApiClient, ISecurityRepository securityRepository, ILoggerInternal logger)
        {
            _logger = logger;
            _priceApiClient = priceApiClient;
            _securityRepository = securityRepository;
        }
        public async Task ExecuteIsins(IEnumerable<string> isins)
        {
            if (isins is null)
            {
                throw new ArgumentNullException(nameof(isins));
            }
            var securities = new List<Security>();
            try
            {
                foreach (var isin in isins)
                {
                    if (isin.Length != 12)
                    {
                        _logger.LogWarn($"Invalid ISIN: {isin}. Should contain 12 alphanumeric characters");
                        continue;
                    }

                    var price = await _priceApiClient.GetPriceFromIsin(isin);
                    if (!price.HasValue)
                    {
                        _logger.LogWarn($"Price from ISIN: {isin} is null.");
                        continue;
                    }
                    securities.Add(new Security { Isin = isin, Price = price.Value });
                }
                if (securities.Any())
                {
                    await _securityRepository.StoreSecurities(securities);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an exception when trying to Exectute ISINS to gather the price", ex);
            }
        }
    }
}
