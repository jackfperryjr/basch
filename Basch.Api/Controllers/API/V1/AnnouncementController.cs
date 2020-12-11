using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Basch.Api.Core.Models;
using Basch.Api.Core.WebApi;
using Basch.Api.Core.Abstractions;

namespace Basch.Api.Controllers.API.V1
{
    [ApiVersion("1")]
    [Authorize]
    public class AnnouncementController : ApiControllerBase
    {
        private readonly IFactory<IQueryable<Announcement>, string> _announcementFactory;

        public AnnouncementController(
            IFactory<IQueryable<Announcement>, string> announcementFactory)
        {
            _announcementFactory = announcementFactory;
        }
        
        [Obsolete]
        [HttpGet("get")]
        public async Task<IActionResult> GetAll(string value, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                var announcements = await _announcementFactory.GetAsync(value, cancellationToken);
                if (announcements.Any())
                {
                    return Ok(announcements);
                }
                else 
                {
                    return NotFound(new
                    {
                        message = "There's nothing to show."
                    });                
                }
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}