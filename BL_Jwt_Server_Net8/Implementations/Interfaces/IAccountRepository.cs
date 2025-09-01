using BL_Jwt_Server_Net8.DTOs;
using BL_Jwt_Server_Net8.Entities;
using static BL_Jwt_Server_Net8.DTOs.CustomResponses;

namespace BL_Jwt_Server_Net8.Implementations.Interfaces
{
    public interface IAccountRepository
    {
        Task<RegistrationResponse> Register(RegisterDTO model);
        Task<LoginResponse> Login(LoginDTO model);
        Task<User> GetUserByEmail(string email);
    }
}
