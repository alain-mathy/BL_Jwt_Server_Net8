using BL_Jwt_Server_Net8.DTOs;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace BL_Jwt_Server_Net8.States
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(Constants.JwtToken))
                {
                    return await Task.FromResult(new AuthenticationState(_anonymous));
                }

                var claims = DecryptToken(Constants.JwtToken);
                if (claims is null)
                {
                    return await Task.FromResult(new AuthenticationState(_anonymous));
                }

                var claimsPrincipal = SetClaimPrincipal(claims);
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }

        public void UpdateAuthenticationState(string jwtToken)
        {
            var claimsPrincipal = new ClaimsPrincipal();
            if (!string.IsNullOrEmpty(jwtToken))
            {
                Constants.JwtToken = jwtToken;
                var getClaims = DecryptToken(jwtToken);
                claimsPrincipal = SetClaimPrincipal(getClaims);
            }
            else
            {
                Constants.JwtToken = null!;
            }
           
            var authState = Task.FromResult(new AuthenticationState(claimsPrincipal));
            NotifyAuthenticationStateChanged(authState);
        }

        private static CustomUserClaims DecryptToken(string jwtToken)
        {
            if (string.IsNullOrEmpty(jwtToken))
            {
                return new CustomUserClaims();
            }

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);

            var name = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var email = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            return new CustomUserClaims(name!, email!);
        }

        private ClaimsPrincipal SetClaimPrincipal(CustomUserClaims claims)
        {
            if (claims.Email is null)
            {
                return new ClaimsPrincipal();
            }

            return new ClaimsPrincipal(new ClaimsIdentity(
                new List<Claim>
                {
                    new Claim(ClaimTypes.Name, claims.Name!),
                    new Claim(ClaimTypes.Email, claims.Email!)
                }, "JwtAuth"));
        }
    }
}
