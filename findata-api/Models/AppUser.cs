using Microsoft.AspNetCore.Identity;

namespace findata_api.Models;

public class AppUser: IdentityUser
{
    public List<Portfolio> Portfolios { get; set; } = [];
}
