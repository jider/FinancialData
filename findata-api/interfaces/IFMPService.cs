using findata_api.Models;

namespace findata_api.interfaces;

public interface IFMPService
{
    Task<Stock?> FindStockBySymbolAsync(string symbol);
}
