using BL_Jwt_Server_Net8.DTOs;
using BL_Jwt_Server_Net8.Entities;
using static BL_Jwt_Server_Net8.DTOs.CustomResponses;

namespace BL_Jwt_Server_Net8.Services
{
    public interface IAccountService
    {
        Task<RegistrationResponse> Register(RegisterDTO model);
        Task<LoginResponse> Login(LoginDTO model);
    }
}
