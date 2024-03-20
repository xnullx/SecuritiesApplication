using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecuritiesApplication.ApiClients
{
    public interface IPriceApiClient
    {
        Task<decimal?> GetPriceFromIsin(string isin);
    }
}
