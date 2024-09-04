using System.Security.Claims;

namespace findata_api.Extensions;

public static class ClaimsExtensions
{
    public static string GetUserName(this ClaimsPrincipal user)
    {
        return user.Claims.SingleOrDefault(claim => claim.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")).Value;
    }
}
