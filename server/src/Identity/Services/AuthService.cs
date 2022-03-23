using Application.Common.Exceptions;
using Application.Contracts.Identity;
using Application.DTO.Account;
using Domain;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly TokenService _tokenService;
        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, TokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }
        private UserDto CreateUserObject(AppUser user)
        {
            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                IsAdmin = user.IsAdmin
            };
        }
        public async Task<UserDto> Login(LoginDto dto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (user == null)
            {
                throw new AuthException("Wrong email or password.");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);

            if (!result.Succeeded)
            {
                throw new AuthException("Wrong email or password.");
            }

            return CreateUserObject(user);
        }

        public async Task<UserDto> Register(RegisterDto dto)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == dto.Email))
            {
                throw new ValidationException(new[] { new ValidationFailure("Email", "Email is taken") });
            }

            if (await _userManager.Users.AnyAsync(x => x.UserName == dto.Username))
            {
                throw new ValidationException(new[] { new ValidationFailure("Username", "Username is taken") });
            }

            var user = new AppUser
            {
                Email = dto.Email,
                UserName = dto.Username,
                IsAdmin = false
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                throw new DatabaseException("Failed to register the user");
            }

            return CreateUserObject(user);
        }
    }
}