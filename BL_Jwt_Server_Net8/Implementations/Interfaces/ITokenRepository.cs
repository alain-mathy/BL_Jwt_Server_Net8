using BL_Jwt_Server_Net8.Entities;

namespace BL_Jwt_Server_Net8.Implementations.Interfaces
{
    public interface ITokenRepository
    {
        string GenerateToken(User user);
    }
}
