using WealthZone.Dto.Stock;
using WealthZone.Models;

namespace WealthZone.Mapper
{
    public static class StockMapper
    {
        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Industry = stockModel.Industry,
                LastDiv = stockModel.LastDiv,
                MarketCap = stockModel.MarketCap,
                Purchase = stockModel.Purchase,
            };
        }


        public static Stock ToStockFromCreateDto(this CreateStockRequestDto StockDto)
        {
            return new Stock
            {
                Symbol = StockDto.Symbol,
                CompanyName = StockDto.CompanyName,
                Industry = StockDto.Industry,
                LastDiv = StockDto.LastDiv,
                MarketCap = StockDto.MarketCap,
                Purchase = StockDto.Purchase,
            };
        }
    }
}
