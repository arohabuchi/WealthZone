using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WealthZone.Data.Interface;
using WealthZone.Dto.Stock;
using WealthZone.Helpers;
using WealthZone.Models;

namespace WealthZone.Data.Repository
{
    public class StockRepoServices : IStockRepo 
    {

        private readonly AppDbContext context;
        public StockRepoServices(AppDbContext _context)
        {
            this.context = _context;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await this.context.stocks.AddAsync(stockModel);
            await this.context.SaveChangesAsync(); 
            return stockModel;  
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel =await context.stocks.FirstOrDefaultAsync(x=>x.Id==id);
            if (stockModel == null)
            {
                return null;
            }
            context.stocks.Remove(stockModel);
            await context.SaveChangesAsync();
            return stockModel; // ?? null;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            var AllStock = context.stocks.Include(c=>c.Comments).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                AllStock = AllStock.Where(c=>c.CompanyName.Contains(query.CompanyName));

            }
            if (!string.IsNullOrWhiteSpace(query.Symbol ))
            {
                AllStock = AllStock.Where(c => c.Symbol.Contains(query.Symbol));

            }

            /////
            ///
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("symbol", StringComparison.OrdinalIgnoreCase))
                {
                    AllStock = query.IsDecsending ? AllStock.OrderByDescending(c => c.Symbol) : AllStock.OrderBy(c => c.Symbol);
                }

            }
            var skipNumber = (query.PageNumber -  1) * query.PageSize;
            return await AllStock.Skip(skipNumber).Take(query.PageSize ).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            var stockDetails = await context.stocks.Include(c => c.Comments).FirstOrDefaultAsync(i=>i.Id==id);
            if (stockDetails == null)
            {
                return null;
            }
            return stockDetails;    
        }

        public Task<bool> StockExist(int id)
        {
            return context.stocks.AnyAsync(x=>x.Id==id);
          
        }

        public async Task<Stock?> UpdateStockAsync(int id, UpdateStockRequestDto stockDto)
        {
            var existingModel = await context.stocks.FirstOrDefaultAsync(x=>x.Id==id);
            if (existingModel == null)
            {
                return null;
            }
            existingModel.Symbol = stockDto.Symbol;
            existingModel.Purchase = stockDto.Purchase;
            existingModel.MarketCap = stockDto.MarketCap;
            existingModel.CompanyName = stockDto.CompanyName;
            existingModel.LastDiv = stockDto.LastDiv;
            existingModel.Industry = stockDto.Industry;

            await context.SaveChangesAsync();
            return existingModel;
        }
    }
}
