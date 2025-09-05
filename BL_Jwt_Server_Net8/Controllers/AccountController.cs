using BL_Jwt_Server_Net8.DTOs;
using BL_Jwt_Server_Net8.Implementations.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static BL_Jwt_Server_Net8.DTOs.CustomResponses;

namespace BL_Jwt_Server_Net8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<RegistrationResponse>> Register(RegisterDTO model)
        {
            var result = await _accountRepository.Register(model);
            return Ok(result);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponse>> Login(LoginDTO model)
        {
            var result = await _accountRepository.Login(model);
            return Ok(result);
        }

        [HttpGet("weather")]
        [Authorize]
        public ActionResult<WeatherForecast[]> GetWeather()
        {
            var startDate = DateOnly.FromDateTime(DateTime.Now);
            var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
            return Ok(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = summaries[Random.Shared.Next(summaries.Length)]
            }).ToArray());
        }
    }
}
