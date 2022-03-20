using Application.Contracts.Identity;
using Application.DTO.Account;
using Application.Responses;
using Domain;
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
        public async Task<Result<UserDto>> Login(LoginDto dto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (user == null) return Result<UserDto>.Failure("Wrong email or password");

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);

            return result.Succeeded ?
                Result<UserDto>.Success(CreateUserObject(user)) :
                Result<UserDto>.Failure("Wrong email or password");
        }

        public async Task<Result<UserDto>> Register(RegisterDto dto)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == dto.Email))
            {
                return Result<UserDto>.Failure("Email is taken");
            }
            if (await _userManager.Users.AnyAsync(x => x.UserName == dto.Username))
            {
                return Result<UserDto>.Failure("Username is taken");
            }

            var user = new AppUser
            {
                Email = dto.Email,
                UserName = dto.Username,
                IsAdmin = false
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            return result.Succeeded ?
                Result<UserDto>.Success(CreateUserObject(user)) :
                Result<UserDto>.Failure("Failed to register");
        }
    }
}