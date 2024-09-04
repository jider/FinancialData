using findata_api.Data;
using findata_api.interfaces;
using findata_api.Models;
using Microsoft.EntityFrameworkCore;

namespace findata_api.Repository;

public class PortfolioRepository(ApplicationDBContext dbContext) : IPortfolioRepository
{
    public async Task<Portfolio> CreateAsync(Portfolio portfolio)
    {
        await dbContext.Portfolios.AddAsync(portfolio);
        await dbContext.SaveChangesAsync();

        return portfolio;
    }

    public async Task<Portfolio?> DeleteAsync(AppUser appUser, string symbol)
    {
        var portfolio = await dbContext.Portfolios
            .FirstOrDefaultAsync(portfolio => portfolio.AppUserId == appUser.Id && portfolio.Stock.Symbol.ToLower() == symbol.ToLower());

        if (portfolio is null) return null;

        dbContext.Portfolios.Remove(portfolio);
        await dbContext.SaveChangesAsync();

        return portfolio;
    }

    public async Task<List<Stock>> GetUserPortfolioAsync(AppUser appUser)
    {        
        return await dbContext.Portfolios
            .Where(portfolio => portfolio.AppUserId == appUser.Id)
            .Select(portfolio => new Stock {
                Id = portfolio.StockId,
                Comments = portfolio.Stock.Comments,
                CompanyName = portfolio.Stock.CompanyName,
                Industry = portfolio.Stock.Industry,
                LastDiv = portfolio.Stock.LastDiv,
                MarketCap = portfolio.Stock.MarketCap,
                Purchase = portfolio.Stock.Purchase,
                Symbol = portfolio.Stock.Symbol,
            }).ToListAsync();
    }
}
