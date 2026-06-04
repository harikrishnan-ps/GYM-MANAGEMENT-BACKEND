using GymManagement.Api.DTOs.Auth;
using GymManagement.Api.Helpers;
using GymManagement.Api.Repositories.Interfaces;
using GymManagement.Api.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Api.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IAdminRepository adminRepository, IConfiguration configuration)
        {
            _adminRepository = adminRepository;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
        {
            var admin = await _adminRepository.GetByEmailAsync(loginDto.Email);
            if (admin == null)
            {
                return null; // Admin not found
            }

            // Verify password using BCrypt
            if (!PasswordHasher.VerifyPassword(loginDto.Password, admin.PasswordHash))
            {
                return null; // Invalid credentials
            }

            // Generate JWT Token
            var token = GenerateJwtToken(admin.AdminId.ToString(), admin.Email, admin.Name, admin.Role);

            return new AuthResponseDto
            {
                Token = token,
                AdminId = admin.AdminId,
                Name = admin.Name,
                Email = admin.Email
            };
        }

        private string GenerateJwtToken(string adminId, string email, string name, string role)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var keyString = jwtSettings["Key"] ?? "SuperSecretGymManagementAPIKey12345!@#$";
            var issuer = jwtSettings["Issuer"] ?? "GymManagementApi";
            var audience = jwtSettings["Audience"] ?? "GymManagementClient";
            var durationMinutes = Convert.ToDouble(jwtSettings["DurationInMinutes"] ?? "120");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, adminId),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim("name", name),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(durationMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
