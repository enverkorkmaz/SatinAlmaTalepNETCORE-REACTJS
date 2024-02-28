using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SatinAlmaTalep.Entity.DTOs.Login;
using SatinAlmaTalep.Entity.DTOs.RegisterDto;
using SatinAlmaTalep.Entity.Entities;
using SatinAlmaTalep.Service.Services.Abstraction;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SatinAlmaTalep.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly ITokenService tokenService;
        private readonly IConfiguration configuration;

        public AuthController(UserManager<User> userManager, RoleManager<Role> roleManager, ITokenService tokenService, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.tokenService = tokenService;
            this.configuration = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginDto loginDto)
        {
            User user = await userManager.FindByEmailAsync(loginDto.Email);
            bool checkPassword = await userManager.CheckPasswordAsync(user, loginDto.Password);
            var roles = await userManager.GetRolesAsync(user);
            JwtSecurityToken token = await tokenService.CreateToken(user, roles[0]);
            string refreshToken = tokenService.GenerateRefreshToken();

            _ = int.TryParse(configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

            await userManager.UpdateAsync(user);
            await userManager.UpdateSecurityStampAsync(user);

            string _token = new JwtSecurityTokenHandler().WriteToken(token);

            await userManager.SetAuthenticationTokenAsync(user, "Default", "AccessToken", _token);

            var response = new
            {
                AccessToken = _token,
                RefreshToken = refreshToken,
                TokenExpiryTime = token.ValidTo,
                Role = roles[0]
                
            };

            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterDto registerDto)
        {
            

            // Yeni bir kullanıcı oluşturun
            var user = new User
            {
                UserName = registerDto.Email,
                FullName = registerDto.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = registerDto.Email,
                NormalizedEmail = registerDto.Email
            };

            // Kullanıcıyı kaydetmeyi dene
            var result = await userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                // "user" adında bir rol yoksa oluşturun
                if (!await roleManager.RoleExistsAsync(registerDto.Role))
                {
                    var createRole = new Role
                    {
                        Name = registerDto.Role,
                        NormalizedName = registerDto.Role,
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    };
                    await roleManager.CreateAsync(createRole);
                }

                // Kullanıcıya "user" rolünü ata
                await userManager.AddToRoleAsync(user, registerDto.Role);

                // Başarılı yanıt döndür
                return Ok();
            }
            else
            {
                // Kullanıcı oluşturma işlemi başarısız oldu, hataları döndür
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(errors);
            }
        }
        [HttpPost]
        public async Task<IActionResult> RefreshToken(string accessToken, string refreshToken)
        {
            var principal = tokenService.GetPrincipalFromExpiredToken(accessToken);
            string email = principal.FindFirstValue(ClaimTypes.Email);

            var user = await userManager.FindByEmailAsync(email);
            var roles = await userManager.GetRolesAsync(user);

            var newAccessToken = await tokenService.CreateToken(user, roles[0]);
            string newRefreshToken = tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await userManager.UpdateAsync(user);

            var response = new
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken
            };

            return Ok(response);
        }


    }
}
