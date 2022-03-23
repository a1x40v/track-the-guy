using Application.DTO.Account;

namespace Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<UserDto> Login(LoginDto dto);
        Task<UserDto> Register(RegisterDto dto);
    }
}
