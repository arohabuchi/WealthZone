using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WealthZone.Data;
using WealthZone.Dto.Stock;
using WealthZone.Mapper;
using WealthZone.Models;

namespace WealthZone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly AppDbContext context;
        public StockController(AppDbContext _context)
        {
            this.context = _context;

        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var stock = await context.stocks.ToListAsync();
            stock.Select(s => s.ToStockDto());
            return Ok(stock);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await context.stocks.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDto();
            await context.stocks.AddAsync(stockModel);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }
        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            var stockModel = await context.stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stockModel == null)
            {
                return NotFound();
            }
            stockModel.Symbol = updateDto.Symbol;
            stockModel.Purchase = updateDto.Purchase;
            stockModel.MarketCap = stockModel.MarketCap;
            stockModel.CompanyName = stockModel.CompanyName;
            stockModel.LastDiv = updateDto.LastDiv;
            stockModel.Industry = stockModel.Industry;

            await context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stockModel =await context.stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stockModel == null)
            {
                return NotFound();
            }
            context.stocks.Remove(stockModel);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
