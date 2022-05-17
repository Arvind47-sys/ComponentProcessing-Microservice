using System.Security.Claims;

namespace API.Extensions
{
    /// <summary>
    /// Extension for ClaimsPrincipal to read the claim from JWT
    /// </summary>
    public static class ClaimsPrincipleExtensions
    {
        public static string GetUserName(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}