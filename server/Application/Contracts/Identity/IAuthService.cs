using Application.DTO.Account;
using Application.Responses;

namespace Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<Result<UserDto>> Login(LoginDto dto);
        Task<Result<UserDto>> Register(RegisterDto dto);
    }
}
