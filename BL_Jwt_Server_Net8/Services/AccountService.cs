using BL_Jwt_Server_Net8.DTOs;
using BL_Jwt_Server_Net8.Entities;
using BL_Jwt_Server_Net8.States;
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

        public async Task<WeatherForecast[]> GetWeatherForecasts()
        {
            if (Constants.JwtToken == "")
            {
                return null;
            }

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Constants.JwtToken);
            return await _httpClient.GetFromJsonAsync<WeatherForecast[]>("api/Account/weather")!;
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
