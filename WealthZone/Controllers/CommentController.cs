using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WealthZone.Data.Interface;
using WealthZone.Data;
using WealthZone.Dto.Stock;
using WealthZone.Mapper;
using WealthZone.Dto.Comment;

namespace WealthZone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase 
    {
        private readonly IStockRepo StockRepo;
        private readonly ICommentRepo commentRepo;
        public CommentController(ICommentRepo _commentRepo, IStockRepo _stockRepo)
        {
            this.commentRepo = _commentRepo;
            StockRepo = _stockRepo;  
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comment = await commentRepo.GetAllAsync();
            var commentDto = comment.Select(s => s.ToCommentDto());
            return Ok(commentDto);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await commentRepo.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentDto commentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(" Validation Error");
            }
            if (!await StockRepo.StockExist(stockId)  ) 
            {
                return BadRequest("stock does not exist");
            }
            var commentModel = commentDto.ToCommentFromCreate(stockId);
            await commentRepo.CreateAsync(commentModel);

            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Updates([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(" Validation Error");
            }
            var comment = await commentRepo.updateAync(id, updateDto.ToCommentFromUpdate());
            if (comment == null)
            {
                return NotFound();
             }

            return Ok(comment.ToCommentDto);
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var commentModel = await commentRepo.DeleteAsync(id);
            if (commentModel == null)
            {
                return NotFound();
            }
            return Ok(commentModel);
        }
    }
}