using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Basch.Api.Core.Abstractions;
using Basch.Api.Core.Models;
using Basch.Api.Core.Extensions;

namespace Basch.Api.Infrastructure.Data
{
    public class TokenFactory : IFactory<JwtSecurityToken, string>
    {
        private ApplicationDbContext _context;
        private IConfiguration _configuration;


        public TokenFactory(
            ApplicationDbContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<JwtSecurityToken> GetAsync(string value, CancellationToken cancellationToken = default(CancellationToken))
        {
            var authClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, "Penelo"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
            var authSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Secret").Value));

            var token = new JwtSecurityToken(
                issuer: "Basch", 
                audience: "Penelo",
                expires: DateTime.Now.AddHours(24),
                claims: authClaims,
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }
    }
}