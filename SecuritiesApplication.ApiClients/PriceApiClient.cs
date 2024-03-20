using Newtonsoft.Json;
using SecuritiesApplication.Helpers;

namespace SecuritiesApplication.ApiClients
{
    public class PriceApiClient : IPriceApiClient
    {
        private readonly ILoggerInternal _logger;
        private readonly HttpClient _httpClient;

        public PriceApiClient(ILoggerInternal logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
        }

        public async Task<decimal?> GetPriceFromIsin(string isin)
        {
            if (isin is null)
            {
                throw new ArgumentNullException(nameof(isin));
            }

            try
            {
                var result = await _httpClient.GetAsync($"https://securities.dataprovider.com/securityprice/{isin}");

                if (!result.IsSuccessStatusCode)
                {
                    _logger.LogError($"Failed to retrieve data for ISIN: {isin}. Status code: {result.StatusCode}");
                    return null;
                }
                var content = await result.Content.ReadAsStringAsync();
                var price = JsonConvert.DeserializeObject<decimal>(content);
                return price;
            }
            catch (Exception ex)
            {
                _logger.LogError($"There was an exception when trying to retrieve data for ISIN: {isin}", ex);
                return null;
            }
        }
    }
}
