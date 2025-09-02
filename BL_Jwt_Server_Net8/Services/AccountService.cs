using BL_Jwt_Server_Net8.DTOs;
using BL_Jwt_Server_Net8.Entities;
using static BL_Jwt_Server_Net8.DTOs.CustomResponses;

namespace BL_Jwt_Server_Net8.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;

        public AccountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LoginResponse> Login(LoginDTO model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Account/login", model);
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            return result!;
        }

        public async Task<RegistrationResponse> Register(RegisterDTO model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Account/register", model);
            var result = await response.Content.ReadFromJsonAsync<RegistrationResponse>();
            return result!;
        }
    }
}
