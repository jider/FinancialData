using findata_api.DTOs.Stock;
using findata_api.Models;

namespace findata_api.Mappers;

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
            Comments = stockModel.Comments.Select(comment => comment.ToCommentDto()).ToList()
        };
    }

    public static Stock ToStockFromCreateDto(this CreateStockRequestDto createStockRequestDto)
    {
        return new Stock
        {
            CompanyName = createStockRequestDto.CompanyName,            
            Industry = createStockRequestDto.Industry,
            LastDiv = createStockRequestDto.LastDiv,
            MarketCap = createStockRequestDto.MarketCap,
            Purchase = createStockRequestDto.Purchase,
            Symbol = createStockRequestDto.Symbol
        };
    }

    public static Stock ToStockFromFMP(this FMPStock fmpStock)
    {
        return new Stock
        {
            CompanyName = fmpStock.CompanyName,
            Industry = fmpStock.Industry,
            LastDiv = (decimal)fmpStock.LastDiv,
            MarketCap = fmpStock.MktCap,
            Purchase = (decimal)fmpStock.Price,
            Symbol = fmpStock.Symbol
        };
    }
}
