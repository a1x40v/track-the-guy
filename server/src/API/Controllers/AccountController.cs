using Application.Contracts.Identity;
using Application.DTO.Account;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto dto)
        {
            var result = await _authService.Login(dto);

            return result.IsSuccess ? Ok(result.Value) : Unauthorized(result.Error);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto dto)
        {
            var result = await _authService.Register(dto);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
    }
}