using AutoMapper;
using BookingERP.Bussiness.Helpers.Interfaces;
using BookingERP.Bussiness.Interfaces;
using BookingERP.Bussiness.Models.Guest;
using BookingERP.Bussiness.Models.Login;
using BookingERP.Bussiness.Models.Manager;
using BookingERP.Common.Enums;
using BookingERP.Common.Exceptions;
using BookingERP.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BookingERP.Bussiness.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IGuestService _guestService;
        private readonly IJwtHelper _jwtHelper;
        private readonly IManagerService _managerService;
        private readonly IMapper _mapper;

        public AuthenticationService(IMapper mapper, UserManager<ApplicationUser> userManager, IGuestService guestService, IJwtHelper jwtHelper, IManagerService managerService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _guestService = guestService;
            _jwtHelper = jwtHelper;
            _managerService = managerService;
        }

        public async Task<LoginResponseModel> Login(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                throw new InvalidLoginException("Wrong Email or password");
            }

            var signingCredentials = _jwtHelper.GetSigningCredentials();
            var claims = await _jwtHelper.GetClaims(user);
            var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();
            var tokenOptions = _jwtHelper.GenerateTokenOptions(signingCredentials, claims, model.RememberMe);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return new LoginResponseModel() { Token = token };
        }

        public async Task RegisterGuest(GuestRegisterModel model)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                throw new EmailNotUniqueException($"User with email: {model.Email} already exists. ");
            }

            ApplicationUser newUser = _mapper.Map<ApplicationUser>(model);

            var result = await _userManager.CreateAsync(newUser, model.Password);
            var resultRole = await _userManager.AddToRoleAsync(newUser, Enums.UserRole.Guest.ToString());

            if (result.Succeeded)
                await _guestService.CreateGuestAsync(newUser, model);

        }

        public async Task RegisterManager(ManagerRegisterModel model)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                throw new EmailNotUniqueException($"User with email: {model.Email} already exists. ");
            }
            ApplicationUser newUser = _mapper.Map<ApplicationUser>(model);

            var result = await _userManager.CreateAsync(newUser, model.Password);
            var resultRole = await _userManager.AddToRoleAsync(newUser, Enums.UserRole.Manager.ToString());

            if (result.Succeeded)
                await _managerService.CreateManagerAsync(newUser, model);

        }
    }
}
