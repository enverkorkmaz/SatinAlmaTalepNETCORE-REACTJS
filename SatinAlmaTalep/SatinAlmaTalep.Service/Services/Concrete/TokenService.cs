using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SatinAlmaTalep.Entity.Entities;
using SatinAlmaTalep.Service.Services.Abstraction;
using SatinAlmaTalep.Service.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SatinAlmaTalep.Service.Services.Concrete
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<User> userManager;
        private readonly TokenSettings settings;

        public TokenService(IOptions<TokenSettings> options,UserManager<User> userManager)
        {
            settings = options.Value;
            this.userManager = userManager;
        }
        public async Task<JwtSecurityToken> CreateToken(User user, string role)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim("role", role)
            };

  
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Secret));
            var token = new JwtSecurityToken(
                issuer:settings.Issuer,
                audience:settings.Audience,
                expires: DateTime.Now.AddMinutes(settings.TokenValidityInMinutes),
                claims:claims,
                signingCredentials: new SigningCredentials(key,SecurityAlgorithms.HmacSha256)
                
                );
            return token;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var  rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            TokenValidationParameters tokenValidationParameters = new()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey=true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Secret)),
                ValidateLifetime = false
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token,tokenValidationParameters,out SecurityToken securityToken);
            if(securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Token bulunamadı.");
            }
            else
            {
                return principal;
            }

        }
    }
}
