using Shared.Dtos;
using Shared.OrderModels;

namespace Services.Abstraction
{
    public interface IAuthenticationService
    {
        public Task<UserResultDto> Login(LoginDto loginDto);
        public Task<UserResultDto> Register(RegisterDto registerDto);
        public Task<UserResultDto> GetUserByEmail(string email);
        public Task<bool> CheckIfEmailExist(string email);
        public Task<AddressDto> UpdateUserAddress (AddressDto addressDto, string email);
        public Task<AddressDto> GetUserAddress (string email);
    }
}
