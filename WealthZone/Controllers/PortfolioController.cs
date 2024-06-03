using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WealthZone.Data.Interface;
using WealthZone.Extension;
using WealthZone.Models;

namespace WealthZone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly IStockRepo stockRepo;
        private readonly IPortfolioRepo portfolioRepo;
        private UserManager<ApplicationUser> userManager;
        
        public PortfolioController(IPortfolioRepo _portfolioRepo, IStockRepo _stockRepo, UserManager<ApplicationUser> _userManager)
        {
            this.userManager = _userManager;
            this.stockRepo = _stockRepo;
            this.portfolioRepo = _portfolioRepo;
            
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();
            var appUser = await userManager.FindByNameAsync(username);
            var userPortfolio = portfolioRepo.GetUserPortfolio(appUser);
            return Ok(userPortfolio);




        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            var username = User.GetUsername();
            var appUser = await userManager.FindByNameAsync(username);
            var stock = await stockRepo.GetBySymbolAsync(symbol);

            if (stock == null)
            {
                return BadRequest("stock not found");
            }
            var userPortfolio = await portfolioRepo.GetUserPortfolio(appUser);
            if (userPortfolio.Any(e => e.Symbol.ToLower() == symbol.ToLower()))
            {
                return BadRequest("stock already added");
            }
            var portfolioModel = new Portfolio
            {
                AppUserId = appUser.Id,
                StockId = stock.Id,

            };
            await portfolioRepo.CreatePortfolioAsync(portfolioModel);
            if (portfolioModel == null)
            {
                return StatusCode(500, "could not create");
            }
            else
            {
                return Created();
            }

        }





        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string symbol)
        {
            var username = User.GetUsername();
            var appUser = await userManager.FindByNameAsync(username);
            //var stock = await stockRepo.GetBySymbolAsync(symbol);

           // if (stock == null)
            //{
            //    return BadRequest("stock not found");
            //}
            var userPortfolio = await portfolioRepo.GetUserPortfolio(appUser);
            var filteredstock = userPortfolio.Where(c =>c.Symbol.ToLower() == symbol.ToLower()).ToList();
            if (filteredstock.Count() == 1)
            {
                await portfolioRepo.DeletePortfolioAsync(appUser, symbol);
            }

            else
            {
                return BadRequest("Stock not in your portfolio");
            }
            return Ok();
        }
    }
}
