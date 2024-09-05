using findata_api.Extensions;
using findata_api.interfaces;
using findata_api.Models;
using findata_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace findata_api.Controllers;

[Route("api/portfolio")]
[ApiController]
public class PortfolioController(    
    IFMPService fmpService,
    IPortfolioRepository portfolioRepository,
    IStockRepository stockRepository,
    UserManager<AppUser> userManager) : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserPortfolio()
    {
        var username = User.GetUserName();
        var appUser = await userManager.FindByNameAsync(username);
        var userPortfolio = await portfolioRepository.GetUserPortfolioAsync(appUser);

        return Ok(userPortfolio);
    }

    [HttpPost("{symbol}")]
    [Authorize]
    public async Task<IActionResult> Create([FromRoute] string symbol)
    {
        var username = User.GetUserName();
        var appUser = await userManager.FindByNameAsync(username);

        var stock = await stockRepository.GetBySymbolAsync(symbol);
        if (stock is null)
        {
            stock = await fmpService.FindStockBySymbolAsync(symbol);
            if (stock is null) return BadRequest("Stock does not exists");

            await stockRepository.CreateAsync(stock);
        }

        var userPortfolio = await portfolioRepository.GetUserPortfolioAsync(appUser);
        if (userPortfolio.Any(stock => stock.Symbol.ToLower() == symbol.ToLower())) return BadRequest("The symbol provided already exists in the portfolio");

        var createdPortfolio = await portfolioRepository.CreateAsync(new Portfolio { AppUserId = appUser.Id, StockId = stock.Id });

        return Created();
    }

    [HttpDelete("{symbol}")]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] string symbol)
    {
        var username = User.GetUserName();
        var appUser = await userManager.FindByNameAsync(username);
        var userPortfolio = await portfolioRepository.GetUserPortfolioAsync(appUser);

        if (userPortfolio.Count == 0) return BadRequest("User has no portfolio");

        var deletedPortfolio = await portfolioRepository.DeleteAsync(appUser, symbol);

        return deletedPortfolio is null
            ? NotFound()
            : NoContent();
    }
}
