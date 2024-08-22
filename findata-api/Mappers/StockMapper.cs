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
            Purchase = stockModel.Purchase
        };
    }
}
