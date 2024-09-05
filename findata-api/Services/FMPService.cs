using findata_api.DTOs.Stock;
using findata_api.interfaces;
using findata_api.Mappers;
using findata_api.Models;
using Newtonsoft.Json;

namespace findata_api.Services;

public class FMPService(HttpClient httpClient, IConfiguration configuration) : IFMPService
{
    public async Task<Stock?> FindStockBySymbolAsync(string symbol)
    {
		try
		{
			var result = await httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/profile/{symbol}?apikey={configuration["FMP:apikey"]}");
			if (!result.IsSuccessStatusCode) return null;

            //var content = await result.Content.ReadFromJsonAsync<Stock?>();
            var content = await result.Content.ReadAsStringAsync();
			var fmpStocks = JsonConvert.DeserializeObject<FMPStock[]>(content);
			if (fmpStocks is null || fmpStocks.Length == 0) return null;

			return fmpStocks[0].ToStockFromFMP();
        }
		catch (Exception ex)
		{
			return null;
		}
    }
}
