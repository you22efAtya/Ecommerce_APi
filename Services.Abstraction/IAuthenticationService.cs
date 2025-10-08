using Shared.Dtos;

namespace Services.Abstraction
{
    public interface IAuthenticationService
    {
        public Task<UserResultDto> Login(LoginDto loginDto);
        public Task<UserResultDto> Register(RegisterDto registerDto);
    }
}
