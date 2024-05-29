using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WealthZone.Data;
using WealthZone.Data.Interface;
using WealthZone.Dto.Stock;
using WealthZone.Mapper;
using WealthZone.Models;

namespace WealthZone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        //private readonly AppDbContext context;
        private readonly IStockRepo stockRepo;
        public StockController(AppDbContext _context, IStockRepo _stockRepo)
        {
            //this.context = _context;
            this.stockRepo = _stockRepo;

        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var stock = await stockRepo.GetAllAsync();
            stock.Select(s => s.ToStockDto());
            return Ok(stock);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await stockRepo.GetByIdAsync(id); 
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
            await stockRepo.CreateAsync(stockModel);

            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }
        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> Updates([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            var stockModel = await stockRepo.UpdateStockAsync(id, updateDto);
            if (stockModel == null)
            {
                return NotFound();
            }
           
            return Ok();
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stockModel =await stockRepo.DeleteAsync(id);
            if (stockModel == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
