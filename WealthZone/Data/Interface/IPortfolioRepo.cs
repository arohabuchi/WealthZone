using WealthZone.Models;

namespace WealthZone.Data.Interface
{
    public interface IPortfolioRepo
    {
        Task<List<Stock>> GetUserPortfolio(ApplicationUser user);   
        Task<Portfolio> CreatePortfolioAsync(Portfolio portfolio);
        Task<Portfolio> DeletePortfolioAsync(ApplicationUser user, string symbol);
    }
}
