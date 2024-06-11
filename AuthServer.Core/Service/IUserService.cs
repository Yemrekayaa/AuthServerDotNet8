using AuthServer.Core.DTOs;
using SharedLibrary.DTOs;

namespace AuthServer.Core.Service
{
    public interface IUserService
    {
        Task<ResponseDto<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto);
        Task<ResponseDto<UserAppDto>> GetUserByNameAsync(string UserName);
        
    }
}