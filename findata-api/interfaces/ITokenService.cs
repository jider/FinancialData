using findata_api.Models;

namespace findata_api.interfaces;

public interface ITokenService
{
    string CreateToken(AppUser appUser);
}
