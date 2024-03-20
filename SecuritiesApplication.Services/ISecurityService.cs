namespace SecuritiesApplication.Services
{
    public interface ISecurityService
    {
        Task ExecuteIsins(IEnumerable<string> isins);
    }
}
