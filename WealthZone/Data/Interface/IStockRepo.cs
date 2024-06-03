using WealthZone.Dto.Stock;
using WealthZone.Helpers;
using WealthZone.Models;

namespace WealthZone.Data.Interface
{
    public interface IStockRepo
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock?> GetBySymbolAsync(string symbol);
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock?> UpdateStockAsync(int id, UpdateStockRequestDto stockDto);
        Task<Stock?> DeleteAsync(int id);
        Task<bool> StockExist(int id);

    }
}
