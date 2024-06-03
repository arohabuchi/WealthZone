using Microsoft.EntityFrameworkCore;
using WealthZone.Data.Interface;
using WealthZone.Models;

namespace WealthZone.Data.Repository
{
    public class PortfolioRepoService : IPortfolioRepo
    {
        private readonly AppDbContext context;
        public PortfolioRepoService(AppDbContext _context)
        {

            this.context = _context;

        }

        public async Task<Portfolio> CreatePortfolioAsync(Portfolio portfolio)
        {
            await context.portfolios.AddAsync(portfolio); 
            await context.SaveChangesAsync();
            return portfolio;

        }

        public async Task<Portfolio> DeletePortfolioAsync(ApplicationUser user, string symbol)
        {
            var portfoliomodel = await context.portfolios.FirstOrDefaultAsync(c => c.AppUserId == user.Id && c.stock.Symbol.ToLower() == symbol.ToLower());
            if (portfoliomodel == null)
            {
                return null;
            }
            context.portfolios.Remove(portfoliomodel);
            await context.SaveChangesAsync();
            return portfoliomodel;

        }

        public async Task<List<Stock>> GetUserPortfolio(ApplicationUser user)
        {
            return await context.portfolios.Where(x => x.AppUserId == user.Id)
                .Select(stock => new Stock
                {
                    Id =stock.StockId,
                    Symbol=stock.stock.Symbol,
                    CompanyName=stock.stock.CompanyName,
                    Purchase =stock.stock.Purchase,
                    LastDiv =stock.stock.LastDiv,
                    Industry=stock.stock.Industry,
                    MarketCap =stock.stock.MarketCap,

                }).ToListAsync();
        }
    }
}
