using findata_api.Models;

namespace findata_api.interfaces;

public interface IPortfolioRepository
{
    Task<Portfolio> CreateAsync(Portfolio portfolio);

    Task<List<Stock>> GetUserPortfolioAsync(AppUser appUser);

    Task<Portfolio?> DeleteAsync(AppUser appUser, string symbol);
}
