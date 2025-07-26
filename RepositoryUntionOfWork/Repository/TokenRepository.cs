using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WalksAPI.RepositoryUntionOfWork.IRepositoryInterfaces;

namespace WalksAPI.RepositoryUntionOfWork.Repository
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration configuration;

        public TokenRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string CreateJwtToken(IdentityUser user, List<string> roles)
        {
            // Validation checks
            if (user == null)
                throw new ArgumentNullException(nameof(user), "User cannot be null");

            if (string.IsNullOrEmpty(user.Email))
                throw new ArgumentException("User email cannot be null or empty");

            if (string.IsNullOrEmpty(user.Id))
                throw new ArgumentException("User ID cannot be null or empty");

            // Validate configuration values
            var jwtKey = configuration["JWT:Key"];
            var jwtIssuer = configuration["JWT:Issuer"];
            var jwtAudience = configuration["JWT:Audience"];

            if (string.IsNullOrEmpty(jwtKey))
                throw new InvalidOperationException("JWT:Key is not configured in appsettings");

            if (string.IsNullOrEmpty(jwtIssuer))
                throw new InvalidOperationException("JWT:Issuer is not configured in appsettings");

            if (string.IsNullOrEmpty(jwtAudience))
                throw new InvalidOperationException("JWT:Audience is not configured in appsettings");

            // Check key length (should be at least 32 characters for HMAC-SHA256)
            if (jwtKey.Length < 32)
                throw new InvalidOperationException("JWT:Key must be at least 32 characters long for security");

            // Create claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Unique token ID
            };

            // Add roles (handle null roles gracefully)
            if (roles != null && roles.Any())
            {
                foreach (var role in roles)
                {
                    if (!string.IsNullOrEmpty(role))
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }
                }
            }

            // Create signing key and credentials
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create token - Use UTC time to avoid timezone issues
            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                notBefore: DateTime.UtcNow, // Token is valid from now
                expires: DateTime.UtcNow.AddMinutes(30), // Use UTC time
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}