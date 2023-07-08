using BookingERP.Bussiness.Helpers.Interfaces;
using BookingERP.Common.Enums;
using BookingERP.Data.Entities;
using BookingERP.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookingERP.Bussiness.Helpers
{
    public class JwtHelper : IJwtHelper
    {

        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _jwtSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IGuestRepository _guestRepository;
        private readonly IManagerRepostirory _managerRepostirory;
        public JwtHelper(IConfiguration configuration, UserManager<ApplicationUser> userManager, IGuestRepository guestRepository, IManagerRepostirory managerRepostirory)
        {
            _configuration = configuration;
            _jwtSettings = _configuration.GetSection("JwtSettings");
            _userManager = userManager;
            _guestRepository = guestRepository;
            _managerRepostirory = managerRepostirory;
        }

        public async Task<List<Claim>> GetClaims(ApplicationUser user)
        {
            

            // Add claims
            var claims = new List<Claim>
            {
                new Claim("Email", user.Email),
                new Claim("UserId", user.Id.ToString()),
            };

            // Add roles
            var roles = (await _userManager.GetRolesAsync(user)).ToList();
            roles.ForEach(x => claims.Add(new Claim("Role", x)));

            // Add client id
            string userClientId = "";
            if (await _userManager.IsInRoleAsync(user, Enums.UserRole.Guest.ToString()))
            {
                var guest = await _guestRepository.GetGuestByUserId(user.Id);
                userClientId = guest.Id.ToString();
                claims.Add(new Claim("Name", $"{guest.Name} {guest.Surname}"));
            }
            if(await _userManager.IsInRoleAsync(user, Enums.UserRole.Manager.ToString()))
            {
                var manager = await _managerRepostirory.GetManagerByUserIdAsync(user.Id);
                userClientId = manager.Id.ToString();
                claims.Add(new Claim("Name", $"{manager.Name} {manager.Surname}"));
                claims.Add(new Claim("HotelId", manager.HotelId.ToString()));

            }

            claims.Add(new Claim("Id", userClientId));

            return claims;
        }

        public JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims, bool RememberMe)
        {
            var tokenOptions = new JwtSecurityToken();
            if (RememberMe)
            {
                tokenOptions = new JwtSecurityToken(
                issuer: _jwtSettings.GetSection("validIssuer").Value,
                audience: _jwtSettings.GetSection("validAudience").Value,
                claims: claims,
                expires: DateTime.Now.AddYears(10),
                signingCredentials: signingCredentials);
            }
            else
            {
                tokenOptions = new JwtSecurityToken(
               issuer: _jwtSettings.GetSection("validIssuer").Value,
               audience: _jwtSettings.GetSection("validAudience").Value,
               claims: claims,
               expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings.GetSection("expiryInMinutes").Value)),
               signingCredentials: signingCredentials);
            }
            return tokenOptions;
        }


        public SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings.GetSection("securityKey").Value);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
    }
}
