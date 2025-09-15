using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using FranchisService.Models.Response;

namespace FranchisService.Helpers
{
    /// <summary>
    /// Helper class for generating JWT tokens
    /// </summary>
    public class JwtTokenHelper
    {
        private readonly IConfiguration _config;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="config"></param>
        public JwtTokenHelper(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Generates a JWT token for the given user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string GenerateToken(UserResponse user)
        {
            var jwtSettings = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpiresInMinutes"]!));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
