using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Service.Contract;

namespace Talabat.Service.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager)
        {
            //header
            //playload
            //Signature
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.Email ??  string.Empty),
                new Claim(ClaimTypes.GivenName,user.DisplayName),
                new Claim(ClaimTypes.MobilePhone,user.PhoneNumber ?? string.Empty)
            };
            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var expirationInMinutes = _configuration["Jwt:expirationInMinutes"];
            if (string.IsNullOrEmpty(expirationInMinutes) || !double.TryParse(expirationInMinutes, out double expiration))
            {
                throw new InvalidOperationException("Invalid expiration configuration.");
            }
            var key = _configuration["Jwt:key"];
            if (string.IsNullOrEmpty(key))
            {
                throw new InvalidOperationException("Invalid key configuration.");
            }
            var token = new JwtSecurityToken
                (
                issuer: _configuration["Jwt:issuer"],
                audience: _configuration["Jwt:audience"],
                claims: authClaims,
                expires: DateTime.UtcNow.AddMinutes(expiration),
                   signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
            //Message "Could not load type 'Microsoft.IdentityModel.Json.Linq.JToken' from assembly 'Microsoft.IdentityModel.Tokens, Version=8.14.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'."    string



        }
    }
}
