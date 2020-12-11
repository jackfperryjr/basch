using System;
using System.Web;
using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Basch.Api.Core.Models;
using Basch.Api.Core.WebApi;
using Basch.Api.Core.Abstractions;
using Basch.Api.Core.Extensions;

namespace Basch.Api.Controllers.API.V1
{
    [ApiVersion("1")]
    [Authorize]
    public class AccountController : ApiControllerBase
    {
        private readonly IFactory<JwtSecurityToken, string> _tokenFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IConfiguration _configuration;

        public AccountController(
            IFactory<JwtSecurityToken, string> tokenFactory,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            _tokenFactory = tokenFactory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }
       
        [Obsolete]
        [HttpGet("penelo")] 
        public async Task<IActionResult> GetPenelo(string value, CancellationToken cancellationToken = new CancellationToken()) 
        { 
            var penelo = _httpContextAccessor.HttpContext.Request.Headers["Origin"];
            
            if (penelo.ToString() == _configuration["Penelo"])
            {
                try
                {
                    var key = _configuration["SimpleWebRTC:Key"];
                    var secret = _configuration["SimpleWebRTC:Secret"];
                    var x = await ApplicationExtensions.Get<Penelo>(key, secret);
                    return Ok(x);
                }
                catch
                {
                    return BadRequest();
                }  
            }
            else
            {
                return BadRequest(new 
                {
                    message = "Sorry, bro."
                });
            }  
        } 

        [Obsolete]
        [AllowAnonymous]
        [HttpGet("token")] 
        public async Task<IActionResult> GetToken(string value, CancellationToken cancellationToken = new CancellationToken()) 
        { 
            var token = await _tokenFactory.GetAsync(value, cancellationToken);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new
            {
                token = tokenString
            });
        } 
    }
}