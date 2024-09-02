using Microsoft.EntityFrameworkCore;
using findata_api.Data;
using findata_api.interfaces;
using findata_api.Models;
using findata_api.DTOs.Stock;

namespace findata_api.Repository;

public class StockRepository(ApplicationDBContext dbContext) : IStockRepository
{
    public async Task<Stock> CreateAsync(Stock stock)
    {
        await dbContext.Stocks.AddAsync(stock);
        await dbContext.SaveChangesAsync();

        return stock;
    }

    public async Task<Stock?> DeleteAsync(int id)
    {
        var stock = await dbContext.Stocks.FindAsync(id);

        if (stock is null) return null;

        dbContext.Stocks.Remove(stock);
        await dbContext.SaveChangesAsync();

        return stock;
    }

    public async Task<bool> ExistAsync(int id)
    {
        return await dbContext.Stocks.AnyAsync(stock => stock.Id == id);
    }

    public async Task<List<Stock>> GetAllAsync()
    {
        return await dbContext.Stocks
            .AsNoTracking()
            .Include(stock => stock.Comments)
            .ToListAsync();
    }

    public async Task<Stock?> GetAsync(int id)
    {
        return await dbContext.Stocks
            .Include(stock => stock.Comments)
            .FirstOrDefaultAsync(stock => stock.Id == id);
    }

    public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto updateStockRequestDto)
    {
        var stock = await dbContext.Stocks.FindAsync(id);

        if (stock is null) return null;

        stock.CompanyName = updateStockRequestDto.CompanyName;
        stock.Industry = updateStockRequestDto.Industry;
        stock.LastDiv = updateStockRequestDto.LastDiv;
        stock.MarketCap = updateStockRequestDto.MarketCap;
        stock.Purchase = updateStockRequestDto.Purchase;
        stock.Symbol = updateStockRequestDto.Symbol;

        await dbContext.SaveChangesAsync();

        return stock;
    }
}
