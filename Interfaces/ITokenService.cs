using Api.Entities;

namespace Api.Interfaces
{
    /// <summary>
    /// Interface for TokenService
    /// </summary>
    public interface ITokenService
    {
        string CreateToken(AppUser appUser);
    }
}