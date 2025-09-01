using BCrypt.Net;
using BL_Jwt_Server_Net8.Data;
using BL_Jwt_Server_Net8.DTOs;
using BL_Jwt_Server_Net8.Entities;
using BL_Jwt_Server_Net8.Implementations.Interfaces;
using Microsoft.EntityFrameworkCore;
using static BL_Jwt_Server_Net8.DTOs.CustomResponses;

namespace BL_Jwt_Server_Net8.Implementations.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;
        private readonly ITokenRepository _tokenRepository;

        public AccountRepository(AppDbContext context, ITokenRepository tokenRepository)
        {
            _context = context;
            _tokenRepository = tokenRepository;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var result = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return result;
        }

        public async Task<LoginResponse> Login(LoginDTO model)
        {
            var result = await GetUserByEmail(model.Email);
            if (result is null)
            {
                return new LoginResponse(false, "Email does not exists.");
            }

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(model.Password, result.Password);
            if (!isValidPassword)
            {
                return new LoginResponse(false, "Invalid password.");
            }

            string token = _tokenRepository.GenerateToken(result);
            return new LoginResponse(true, "Login successful.", token);
        }

        public async Task<RegistrationResponse> Register(RegisterDTO model)
        {
            var result = await GetUserByEmail(model.Email);
            if (result is not null)
            {
                return new RegistrationResponse(false, "Email already exists.");
            }

            _context.Users.Add(new User
            {
                Name = model.Name,
                Email = model.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password)
            });

            await _context.SaveChangesAsync();
            return new RegistrationResponse(true, "Registration successful.");
        }
    }
}
