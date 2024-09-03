using findata_api.Common.Models;
using findata_api.DTOs.Stock;
using findata_api.Models;

namespace findata_api.interfaces;

public interface IStockRepository
{
    Task<Stock> CreateAsync(Stock stock);

    Task<Stock?> DeleteAsync(int id);

    Task<bool> ExistAsync(int  id);

    Task<List<Stock>> GetAllAsync(QueryFilter queryFilter);

    Task<Stock?> GetAsync(int id);

    Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto updateStockRequestDto);
}
