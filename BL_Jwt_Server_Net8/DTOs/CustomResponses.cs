namespace BL_Jwt_Server_Net8.DTOs
{
    public class CustomResponses
    {
        public record RegistrationResponse(bool Flag, string Message = null!);
        public record LoginResponse(bool Flag = false, string Message = null!, string Token = null!);
    }
}
