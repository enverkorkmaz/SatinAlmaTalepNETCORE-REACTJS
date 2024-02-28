using SatinAlmaTalep.Entity.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SatinAlmaTalep.Service.Services.Abstraction
{
    public interface ITokenService
    {
        Task<JwtSecurityToken> CreateToken(User user, string role);
        string GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
    }
}
