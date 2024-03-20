using SecuritiesApplication.Entities;

namespace SecuritiesApplication.Repositories
{
    public interface ISecurityRepository
    {
        Task StoreSecurities(IEnumerable<Security> securities);
    }
}
