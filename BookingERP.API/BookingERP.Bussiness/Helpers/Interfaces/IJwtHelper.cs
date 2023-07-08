using BookingERP.Data.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BookingERP.Bussiness.Helpers.Interfaces
{
    public interface IJwtHelper
    {
        SigningCredentials GetSigningCredentials();
        Task<List<Claim>> GetClaims(ApplicationUser user);
        JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims, bool RememberMe);

    }
}
