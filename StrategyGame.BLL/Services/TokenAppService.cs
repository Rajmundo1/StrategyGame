using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StrategyGame.BLL.Interfaces;
using StrategyGame.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.BLL.Services
{
    public class TokenAppService : ITokenAppService
    {
        private readonly IConfiguration configuration;

        public TokenAppService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<string> CreateNormalAccessToken(User user)
        {
            var claims = new[]
{
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.Id)),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Authentication")["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
               configuration.GetSection("Authentication")["JwtNormalIssuer"],
               configuration.GetSection("Authentication")["JwtNormalIssuer"],
               claims,
               expires: DateTime.Now.AddHours(1),
               signingCredentials: creds
            );
            return await Task.Run(() => new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
